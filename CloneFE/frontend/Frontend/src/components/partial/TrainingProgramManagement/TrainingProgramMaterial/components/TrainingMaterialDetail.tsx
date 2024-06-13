import { Flex, Text, Link } from "@chakra-ui/react";
import { MdOutlineDeleteForever } from "react-icons/md";
import { LuPencil } from "react-icons/lu";
import style from "../style.module.scss";
import { TrainingMaterial } from "../models/trainingMaterial.model";
import { format } from "date-fns";
import React, { useState } from "react";
import ConfirmPopup from "./ConfirmPopup";

interface TrainingMaterialDetailProps {
  setFileName: (fileName: string) => void;
  unitChapterName: string;
  trainingMaterial: TrainingMaterial;
  handleDelete: (fileName: string) => void;
  handleDownload: (trainingMaterialId: string, fileName: string) => void;
  setProcessingUpdate: (c: boolean) => void;
  setTrainingMaterialId: (id: string) => void;
}

const TrainingMaterialDetail: React.FC<TrainingMaterialDetailProps> = (
  props
) => {
  const [open, setOpen] = useState<boolean>(false);

  return (
    <React.Fragment>
      {open && (
        <ConfirmPopup
          open={open}
          setOpen={setOpen}
          fileName={props.trainingMaterial.fileName}
          handleDelete={props.handleDelete}
        />
      )}
      <Flex
        justifyContent="space-beetween"
        alignItems="center"
        className={style.materialDetail}
      >
        <Flex flex={1}>
          <Link
            color="blue.600"
            href="#"
            textDecoration="underline"
            onClick={() =>
              props.handleDownload(
                props.trainingMaterial.trainingMaterialId,
                props.trainingMaterial.fileName
              )
            }
          >
            {props.trainingMaterial.fileName}
          </Link>
        </Flex>
        <Flex flex={1.2} justifyContent="flex-end" alignItems={"center"}>
          {props.trainingMaterial.modifiedBy !== null &&
          props.trainingMaterial.modifiedBy !== "" ? (
            <Text fontStyle="italic" mr={1}>
              {" "}
              by {props.trainingMaterial.modifiedBy}
            </Text>
          ) : (
            props.trainingMaterial.createdBy != null && (
              <Text fontStyle="italic" mr={1}>
                {" "}
                by {props.trainingMaterial.createdBy}
              </Text>
            )
          )}

          {props.trainingMaterial.modifiedDate != null ? (
            <Text fontStyle="italic">
              {" "}
              on {format(props.trainingMaterial.modifiedDate, "dd/MM/yyyy")}
            </Text>
          ) : (
            props.trainingMaterial.createdDate && (
              <Text fontStyle="italic">
                {" "}
                on {format(props.trainingMaterial.createdDate, "dd/MM/yyyy")}
              </Text>
            )
          )}
          <Flex alignItems="flex-end" className={style.actionMenu}>
            <div className={style.iconButton}>
              <LuPencil
                className={style.icon}
                size={22}
                onClick={() => {
                  props.setProcessingUpdate(true);
                  props.setFileName(props.trainingMaterial.fileName);
                  props.setTrainingMaterialId(
                    props.trainingMaterial.trainingMaterialId
                  );
                }}
              />
            </div>
            <div
              className={style.iconButton}
              style={{ marginRight: "0.5rem" }}
              onClick={() => setOpen(true)}
            >
              <MdOutlineDeleteForever className={style.icon} size={31} />
            </div>
          </Flex>
        </Flex>
      </Flex>
    </React.Fragment>
  );
};

export default TrainingMaterialDetail;
