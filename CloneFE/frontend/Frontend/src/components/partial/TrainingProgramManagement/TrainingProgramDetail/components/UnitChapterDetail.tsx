import { UnitChapter } from "../models/unitChapter.model";
import style from "../style.module.scss";
import { IoDocumentTextOutline } from "react-icons/io5";
import { ChakraProvider, useDisclosure } from "@chakra-ui/react";
import MaterialPopup from "../../TrainingProgramMaterial/components/TrainingMaterial";
import { UploadMaterialRequest } from "../../TrainingProgramMaterial/payloads/requests/uploadMaterialRequest.model";
import axiosMultipartForm from "../../api/axiosMultipartForm";
import { toast } from "react-toastify";
import { useGlobalContext } from "../../contexts/DataContext";
import DeleteMaterialRequest from "../../TrainingProgramMaterial/payloads/requests/deleteMaterialRequest.model";
import { ThemeProvider, createTheme } from "@mui/material/styles";
import axiosAuth from "../../api/axiosAuth";
import UpdateMaterialRequest from "../../TrainingProgramMaterial/payloads/requests/updateMaterialRequest.model";

interface UnitChapterDetailProps {
  dayNo: number;
  unitNo: number;
  syllabusId: string;
  unitChapter: UnitChapter;
}

const createUploadMaterialRequest = (
  dayNo: number,
  unitNo: number,
  chapterNo: number,
  syllabusId: string,
  file: File,
  createdBy: string
): UploadMaterialRequest => {
  return { dayNo, unitNo, chapterNo, syllabusId, file, createdBy };
};

const createDeleteMaterialRequest = (
  dayNo: number,
  unitNo: number,
  chapterNo: number,
  syllabusId: string,
  fileName: string
): DeleteMaterialRequest => {
  return { dayNo, unitNo, chapterNo, syllabusId, fileName };
};

const createUpdateMaterialRequest = (
  syllabusId: string,
  trainingMaterialId: string,
  dayNo: number,
  unitNo: number,
  chapterNo: number,
  fileName: string,
  modifiedBy: string,
  file?: File | undefined
): UpdateMaterialRequest => {
  return {
    syllabusId,
    trainingMaterialId,
    dayNo,
    unitNo,
    chapterNo,
    fileName,
    modifiedBy,
    file,
  };
};

const UnitChapterDetail: React.FC<UnitChapterDetailProps> = (props) => {
  const { isOpen, onOpen, onClose } = useDisclosure();
  const { refreshData } = useGlobalContext();

  // Handle methods
  const handleUpload = async (file: File) => {
    // create instance of UploadMaterialRequest
    const uploadMaterialRequest: UploadMaterialRequest =
      createUploadMaterialRequest(
        props.dayNo,
        props.unitNo,
        props.unitChapter.chapterNo,
        props.syllabusId,
        file,
        localStorage.getItem("username")
      );

    // proccess upload file
    try {
      const formData = new FormData();
      formData.append("DayNo", uploadMaterialRequest.dayNo.toString());
      formData.append("UnitNo", uploadMaterialRequest.unitNo.toString());
      formData.append("ChapterNo", uploadMaterialRequest.chapterNo.toString());
      formData.append(
        "SyllabusId",
        uploadMaterialRequest.syllabusId.toString()
      );
      formData.append("File", uploadMaterialRequest.file);
      formData.append("CreatedBy", uploadMaterialRequest.createdBy);

      // Call API
      await axiosMultipartForm
        .post("/trainingprograms/materials", formData)
        .then(async (response) => {
          if (response.status == 200) {
            toast.success(`Upload new training material successfully`);
            // refresh data
            await refreshData();
          } else {
            toast.error("Upload failed.");
          }
        })
        .catch((error) => {
          toast.error("Upload failed: ", error.message);
        });
    } catch (error) {
      if (error instanceof Error) {
      } else {
      }
    }

  };

  const handleDelete = async (fileName: string) => {
    // create instance of DeleteMaterialRequest
    const deleteMaterialRequest: DeleteMaterialRequest =
      createDeleteMaterialRequest(
        props.dayNo,
        props.unitNo,
        props.unitChapter.chapterNo,
        props.syllabusId,
        fileName
      );

    // Call API
    await axiosAuth
      .delete(
        // Delete URL
        "/trainingprograms/materials",
        // Request object
        {
          params: {
            ...deleteMaterialRequest,
          },
        }
      )
      .then(async (response) => {
        if (response.status == 200) {
          toast.success(`Delete file ${fileName} successfully`);
          // refresh data
          await refreshData();
        } else {
          toast.error("Delete failed.");
        }
      })
      .catch((error) => {
        if (error.response.status == 404) {
          toast.error(`Not found any file match ${fileName}`);
        }
        if (error instanceof Error) {
        } else {
        }
      });
  };

  const handleUpdate = async (
    fileName: string,
    file: File | null,
    trainingMaterialId: string
  ) => {
    const updateMaterialRequest = createUpdateMaterialRequest(
      props.syllabusId,
      trainingMaterialId,
      props.dayNo,
      props.unitNo,
      props.unitChapter.chapterNo,
      fileName,
      localStorage.getItem("username"),
      file ? file : null!
    );

    const formData = new FormData();
    formData.append("DayNo", updateMaterialRequest.dayNo.toString());
    formData.append("UnitNo", updateMaterialRequest.unitNo.toString());
    formData.append("ChapterNo", updateMaterialRequest.chapterNo.toString());
    formData.append("SyllabusId", updateMaterialRequest.syllabusId.toString());
    formData.append("FileName", updateMaterialRequest.fileName.toString());
    formData.append(
      "TrainingMaterialId",
      updateMaterialRequest.trainingMaterialId
    );
    formData.append("ModifiedBy", updateMaterialRequest.modifiedBy);
    

    if (
      updateMaterialRequest.file !== undefined &&
      updateMaterialRequest.file !== null
    ) {
      formData.append("File", updateMaterialRequest.file);
    }


    // Call API
    await axiosMultipartForm
      .put(
        // Delete URL
        "/trainingprograms/materials",
        // Request object
        formData
      )
      .then(async (response) => {
        if (response.status == 200) {
          toast.success(`Update file ${fileName} successfully`);
          // refresh data
          await refreshData();
        } else {
          toast.error("Update failed.");
        }
      })
      .catch((error) => {
        if (error.response.status == 404) {
          toast.error(`Not found any file match ${fileName}`);
        }
        if (error instanceof Error) {
        } else {
        }
      });
  };

  const handleDownload = async (
    trainingMaterialId: string,
    fileName: string
  ) => {
    const link = document.createElement("a");
    link.target = "_blank";
    link.download = fileName;

    await axiosAuth
      .get(`/trainingprograms/materials/${trainingMaterialId}`, {
        responseType: "blob",
      })
      .then((res) => {
        link.href = URL.createObjectURL(
          new Blob([res.data], { type: "application/octet-stream" })
        );
        link.click();
      })
      .catch((error) => {
        if (error instanceof Error) {
          toast.error("File not found");
        } else {
        }
      });
  };

  return (
    <div className={style.unitChaper} style={{ width: "99%" }}>
      <span>{props.unitChapter.name}</span>
      <div className={style.unitChapterDetail}>
        <p className={style.code}>{props.unitChapter?.outputStandard?.code}</p>
        <p className={style.duration}>{props.unitChapter.duration}mins</p>
        <p style={{ marginLeft: "1rem", marginRight: "1rem" }}>
          {props.unitChapter.isOnline ? (
            <span className={style.isOnline}>Online</span>
          ) : (
            <span className={style.isOffline} style={{ fontWeight: "normal" }}>
              Offline
            </span>
          )}
        </p>
        <p className={style.icon} style={{ marginRight: "-2em" }}>
          <img
            src={props.unitChapter.deliveryType.icon}
            alt="Link Icon"
            width={24}
            height={24}
          />
        </p>
        <p
          onClick={(e) => {
            e.preventDefault();
            onOpen();
          }}
          className={style.icon}
        >
          <IoDocumentTextOutline size={27} />
          <ThemeProvider theme={createTheme({})}>
            <ChakraProvider>
              <MaterialPopup
                isOpen={isOpen}
                onClose={onClose}
                dayNo={props.dayNo}
                unitNo={props.unitNo}
                unitChapter={props.unitChapter}
                unitChapterName={props.unitChapter.name}
                trainingMaterials={props.unitChapter.trainingMaterials}
                handleUpload={handleUpload}
                handleDelete={handleDelete}
                handleUpdate={handleUpdate}
                handleDownload={handleDownload}
              />
            </ChakraProvider>
          </ThemeProvider>
        </p>
      </div>
    </div>
  );
};

export default UnitChapterDetail;
