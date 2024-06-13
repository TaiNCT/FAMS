import {
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalFooter,
  ModalBody,
  Flex,
  Text,
} from "@chakra-ui/react";
import { Button } from "@chakra-ui/react";
import { TrainingMaterial } from "../models/trainingMaterial.model";
import TrainingMaterialDetail from "./TrainingMaterialDetail";
import { IoMdCloseCircleOutline } from "react-icons/io";
import style from "../style.module.scss";
import { UnitChapter } from "../../TrainingProgramDetail/models/unitChapter.model";
import { useState } from "react";
import { Form, FormGroup } from "react-bootstrap";
import { Input, Label } from "reactstrap";
import { FaFile } from "react-icons/fa";
import { format } from "date-fns";
import { FaUserPen } from "react-icons/fa6";
import { FaCalendarAlt } from "react-icons/fa";
import { MdOutlineFilePresent } from "react-icons/md";
import React from "react";

interface MaterialProps {
  dayNo: number;
  unitNo: number;
  unitChapter: UnitChapter;
  isOpen: boolean;
  unitChapterName: string;
  trainingMaterials: TrainingMaterial[];
  onClose: () => void;
  handleUpload: (file: File) => void;
  handleDelete: (fileName: string) => void;
  handleUpdate: (
    fileName: string,
    file: File | null,
    trainingMaterialId: string
  ) => void;
  handleDownload: (trainingMaterilId: string, fileName: string) => void;
}

const MaterialPopup: React.FC<MaterialProps> = ({
  isOpen,
  onClose,
  unitChapterName,
  trainingMaterials,
  unitNo,
  unitChapter,
  dayNo,
  handleUpload,
  handleDelete,
  handleUpdate,
  handleDownload,
}) => {
  const [processingUpdate, setProcessingUpdate] = useState<boolean>(false);
  const [fileName, setFileName] = useState<string>("");
  const [selectedfileName, setSelectedFileName] = useState<string>("");
  const [updateFile, setUpdateFile] = useState<File | null>(null);
  const [trainingMaterialId, setTrainingMaterialId] = useState<string>("");

  const fileInputRef = React.useRef<HTMLInputElement>(null);

  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const fileList = event.target.files;

    if (fileList && fileList.length > 0) {
      handleUpload(fileList[0]);
    }
  };

  const handleUpdateFileChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    const fileList = event.target.files;

    if (fileList && fileList.length > 0) {
      setUpdateFile(fileList[0]);
      setSelectedFileName(fileList[0].name);
      setFileName(fileList[0].name);
    }
  };

  const handleFileNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const updateName = event.target.value;
    // const lastIndex = updateName.lastIndexOf(".");
    // const filenameWithoutExtension = updateName.substring(0, lastIndex);
    setFileName(updateName);
  };

  const handleButtonClick = () => {
    fileInputRef.current?.click();
  };

  return (
    <div className={style.modal}>
      <Modal
        isOpen={isOpen}
        onClose={onClose}
        autoFocus={false}
        returnFocusOnClose={false}
      >
        <ModalOverlay />
        <ModalContent
          maxW="50%"
          style={{ top: "15%" }}
          className={style.modelContent}
        >
          {!processingUpdate ? (
            <>
              <ModalHeader className={style.header}>
                Day {dayNo}
                <Button
                  bg="none"
                  p={0}
                  color="white"
                  fontSize="25px"
                  position="absolute"
                  right={0}
                  top={1}
                  _hover={{ background: "none" }}
                  onClick={onClose}
                >
                  <IoMdCloseCircleOutline />
                </Button>
              </ModalHeader>
              <ModalBody style={{ fontSize: "1.3rem" }}>
                <div className={style.unitContainer}>
                  <div className={style.unitTitle}>Unit {unitNo}</div>
                  <div className={style.chapter}>{unitChapter.name}</div>
                </div>
                <div
                  className={style.grayBackgroundDiv}
                  style={{ fontSize: "18px" }}
                >
                  {trainingMaterials.length ? (
                    <div>
                      <Text style={{ fontWeight: "bold" }}>
                        {unitChapterName}
                      </Text>
                      {trainingMaterials.map((tm) => (
                        <TrainingMaterialDetail
                          unitChapterName={unitChapterName}
                          trainingMaterial={tm}
                          key={tm.id}
                          handleDelete={handleDelete}
                          handleDownload={handleDownload}
                          setFileName={setFileName}
                          setProcessingUpdate={setProcessingUpdate}
                          setTrainingMaterialId={setTrainingMaterialId}
                        />
                      ))}
                    </div>
                  ) : (
                    <p>Not found any training material</p>
                  )}
                </div>
              </ModalBody>
              <ModalFooter>
                <Flex justifyContent="center" width="full">
                  <label htmlFor="fileInput" className={style.footer}>
                    <input
                      type="file"
                      id="fileInput"
                      onChange={handleFileChange}
                      style={{ display: "none" }}
                    />
                    Upload new
                  </label>
                </Flex>
              </ModalFooter>
            </>
          ) : (
            <>
              <ModalHeader className={style.header}>
                Update training material
                <Button
                  bg="none"
                  p={0}
                  color="white"
                  fontSize="25px"
                  position="absolute"
                  right={0}
                  top={1}
                  _hover={{ background: "none" }}
                  onClick={onClose}
                >
                  <IoMdCloseCircleOutline />
                </Button>
              </ModalHeader>
              <ModalBody>
                
                <div
                  className={style.updateMaterialBox}
                  style={{ fontSize: "18px" }}
                >
                  <Form className={style.form}>
                    <FormGroup className={style.formGroup}>
                      <div className={style.formLabel}>
                        <Label for="filename">FileName:</Label>
                      </div>
                      <div
                        className={style.formControl}
                        style={{ position: "relative" }}
                      >
                        <Input
                          type="text"
                          id="filename"
                          value={fileName}
                          onChange={(e) => handleFileNameChange(e)}
                          placeholder="File name"
                        />
                        <div
                          style={{
                            position: "absolute",
                            top: "50%",
                            right: "10px",
                            transform: "translateY(-50%)",
                          }}
                        >
                          <FaFile />
                        </div>
                      </div>
                    </FormGroup>
                    <FormGroup className={style.formGroup}>
                      <div className={style.formLabel}>
                        <Label for="modifyBy">Modify By:</Label>
                      </div>
                      <div
                        className={style.formControl}
                        style={{ position: "relative" }}
                      >
                        <Input
                          type="text"
                          id="modifyBy"
                          value={localStorage.getItem("username")}
                          placeholder="Modify By"
                          readOnly
                        />
                        <div
                          style={{
                            position: "absolute",
                            top: "50%",
                            right: "10px",
                            transform: "translateY(-50%)",
                          }}
                        >
                          <FaUserPen />
                        </div>
                      </div>
                    </FormGroup>
                    <FormGroup className={style.formGroup}>
                      <div className={style.formLabel}>
                        <Label for="modifyDate">Modify Date:</Label>
                      </div>
                      <div
                        className={style.formControl}
                        style={{ position: "relative" }}
                      >
                        <Input
                          type="date"
                          id="modifyDate"
                          value={format(new Date(), "yyyy-MM-dd")}
                          placeholder="Modify Date"
                          readOnly
                        />
                        <div
                          style={{
                            position: "absolute",
                            top: "50%",
                            right: "10px",
                            transform: "translateY(-50%)",
                          }}
                        >
                          <FaCalendarAlt />
                        </div>
                      </div>
                    </FormGroup>
                    <FormGroup className={style.formGroup}>
                      <div className={style.formLabel}>
                        <Label>Change File:</Label>
                      </div>
                      <div
                        className={style.formControl}
                        style={{ marginTop: "1.3rem" }}
                      >
                        <input
                          type="file"
                          id="file"
                          name="file"
                          onChange={handleUpdateFileChange}
                          readOnly
                          style={{ display: "none" }}
                          ref={fileInputRef}
                        />
                        <button
                          type="button"
                          onClick={handleButtonClick}
                          className="btn btn-primary"
                        >
                          <MdOutlineFilePresent size={25} />
                          {selectedfileName ? selectedfileName : "Select file"}
                        </button>
                      </div>
                    </FormGroup>
                  </Form>
                </div>
              </ModalBody>
              <ModalFooter>
                <Flex justifyContent="center" width="full">
                  <Button
                    onClick={() => {
                      setFileName("");
                      setSelectedFileName("");
                      setUpdateFile(null);
                      setProcessingUpdate(false);
                    }}
                    backgroundColor={"none"}
                    color={"red"}
                    style={{ textDecoration: "underline" }}
                  >
                    Cancel
                  </Button>
                  <Button
                    onClick={() => {
                      handleUpdate(fileName, updateFile, trainingMaterialId);
                      setProcessingUpdate(false);
                    }}
                    backgroundColor={"#111e2e"}
                    color={"white"}
                    ml={8}
                  >
                    Update
                  </Button>
                </Flex>
              </ModalFooter>
            </>
          )}
        </ModalContent>
      </Modal>
    </div>
  );
};
export default MaterialPopup;
