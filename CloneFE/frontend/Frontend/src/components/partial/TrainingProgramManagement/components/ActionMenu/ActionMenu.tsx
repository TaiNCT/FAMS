import React, { FC, memo, useCallback, useState } from "react";
import {
  Box,
  Button,
  Flex,
  Popover,
  PopoverTrigger,
  PopoverContent,
  PopoverHeader,
  PopoverBody,
  Text,
  useDisclosure,
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalBody,
  ModalCloseButton,
  ModalFooter,
} from "@chakra-ui/react";
import {
  MdOutlineMoreHoriz,
  MdOutlineSnippetFolder,
  MdOutlineDeleteForever,
} from "react-icons/md";
import { HiOutlineDocumentDuplicate } from "react-icons/hi";
import { IoMdCloseCircleOutline } from "react-icons/io";
import { BiHide } from "react-icons/bi";
import { LuPencil } from "react-icons/lu";
import * as TrainingProgramService from "../../TrainingProgramList/services/TrainingProgramService";
import { toast } from "react-toastify";
import { useTrainingProgramDataContext } from "../../contexts/DataContext";
import { getOptions } from "../../TrainingProgramList/utils/functionHelper";
import EditProgramForm from "../../TrainingProgramList/components/EditProgramForm";
import { updateTrainingProgram } from "../../TrainingProgramList/models/requests/updateTrainingProgram.model";
import { TrainingProgram } from "../../TrainingProgramDetail/models/trainingprogram.model";
import { useNavigate } from "react-router-dom";
import GlobalLoading from "../../../../global/GlobalLoading.jsx";
import style from "./style.module.scss";

interface ActionMenuProps {
  isDetail: boolean;
  setItem?: (tp: TrainingProgram) => void;
  trainingProgramCode: string;
  trainingProgramName: string;
  id: number;
  status: string;
}

const ActionMenu: FC<ActionMenuProps> = memo(
  ({
    isDetail,
    setItem,
    trainingProgramCode,
    trainingProgramName,
    id,
    status,
  }) => {
    const [isLoading, setIsLoading] = useState(false);
    const [dataUpdate, setDataUpdate] = useState<TrainingProgram | undefined>();

    const {
      isOpen: isOpenConfirmModal,
      onOpen: onOpenConfirmModal,
      onClose: onCloseConfirmModal,
    } = useDisclosure();
    const {
      isOpen: isOpenUpdateModal,
      onOpen: onOpenUpdateModal,
      onClose: onCloseUpdateModal,
    } = useDisclosure();
    const {
      setData,
      setTotalPages,
      setCurrentPage,
      setRowsPerPageOption,
      currentPage,
      rowsPerPage,
    } = useTrainingProgramDataContext();

    const [selectedAction, setSelectedAction] = useState<string>("");
    const [confirmMessage, setConfirmMessage] = useState("");

    const isDraft = status === "Draft";
    const primaryColor = "#285D9A";

    const navigate = useNavigate();

    const handleAction = useCallback(
      async (action: string, value?: updateTrainingProgram) => {
        try {
          let result;
          let toastMessage = `${
            action.charAt(0).toUpperCase() + action.slice(1)
          } training program successfully`;

          switch (action) {
            case "duplicate":
              result = await TrainingProgramService.duplicateTrainingProgram(
                trainingProgramCode,
                localStorage.getItem("username")
              );
              break;
            case "de-active":
              result = await TrainingProgramService.updateStatus(
                trainingProgramCode
              );
              break;
            case "delete":
              result = await TrainingProgramService.deleteTrainingProgram(
                trainingProgramCode
              );
              if (isDetail) {
                navigate("/trainingprogram", { state: { toastMessage } });
                return;
              }
              break;
            case "update":
              result = await TrainingProgramService.updateTrainingProgram(
                value
              );
              onCloseUpdateModal();
              break;
            case "download-material":
              await TrainingProgramService.downloadMaterial(
                trainingProgramCode,
                trainingProgramName
              );
              return;
            default:
              return;
          }

          if (result !== undefined && result.statusCode === 200) {
            if (!isDetail) {
              const updatedList = await TrainingProgramService.paginationList(
                currentPage,
                rowsPerPage
              );
              if (updatedList.totalRecord % 5 === 0 && action === "delete") {
                setCurrentPage(currentPage - 1);
              } else {
                setData(updatedList.list);
                setTotalPages(updatedList.totalPage);
                setRowsPerPageOption(getOptions(updatedList.totalRecord));
              }
            } else if (action !== "delete") {
              result = await TrainingProgramService.getTrainingProgramDetail(
                trainingProgramCode
              );
              setItem?.(result);
            }
            if (!isDetail || action !== "delete") {
              toast.success(toastMessage, { autoClose: 2500 });
            }
          }
        } catch (error) {
          toast.error(`An error occurred while ${action} training program`, {
            autoClose: 2500,
          });
        }
      },
      [
        trainingProgramCode,
        currentPage,
        rowsPerPage,
        setData,
        setTotalPages,
        setCurrentPage,
        navigate,
        isDetail,
      ]
    );

    const handleOpenConfirmModal = useCallback(
      (action: string) => {
        setSelectedAction(action);
        setConfirmMessage(
          `Are you sure you want to ${action} this training program?`
        );
        onOpenConfirmModal();
      },
      [onOpenConfirmModal]
    );

    const handleConfirmAction = useCallback(() => {
      if (selectedAction) {
        handleAction(selectedAction);
      }
      onCloseConfirmModal();
    }, [selectedAction, handleAction, onCloseConfirmModal]);

    const handleOpenUpdateModal = useCallback(async () => {
      setIsLoading(true);
      try {
        const response =
          await TrainingProgramService.getTrainingProgramSyllabus(
            trainingProgramCode
          );
        if (response !== null) {
          setDataUpdate(response);
        }
        onOpenUpdateModal();
      } catch (error) {
        console.error("There was an error!", error);
        setIsLoading(false);
      } finally {
        setTimeout(() => setIsLoading(false), 1000);
      }
    }, [id, onOpenUpdateModal]);

    const handleDownloadMaterial = useCallback(() => {
      handleAction("download-material");
    }, []);

    return (
      <>
        <Popover>
          <PopoverTrigger>
            <Button
              className={isDetail ? style.titleMenu : ""}
              cursor="pointer"
              fontSize="25px"
              bg="none"
              borderRadius="full"
              p={0}
            >
              <MdOutlineMoreHoriz />
            </Button>
          </PopoverTrigger>
          <PopoverContent borderRadius="16px" zIndex={999}>
            <PopoverHeader
              className={isDetail ? style.fontNormal : ""}
              color={primaryColor}
              fontWeight="bold"
            >
              Manage
            </PopoverHeader>
            <PopoverBody>
              {[
                {
                  icon: MdOutlineSnippetFolder,
                  label: "Training material",
                  action: () => handleDownloadMaterial(),
                },
                {
                  icon: LuPencil,
                  label: "Edit program",
                  action: () => handleOpenUpdateModal(),
                },
                {
                  icon: HiOutlineDocumentDuplicate,
                  label: "Duplicate program",
                  action: () => handleOpenConfirmModal("duplicate"),
                },
                {
                  icon: BiHide,
                  label: "De-activate program",
                  action: () => handleOpenConfirmModal("de-active"),
                  isDisabled: isDraft,
                },
                {
                  icon: MdOutlineDeleteForever,
                  label: "Delete program",
                  action: () => handleOpenConfirmModal("delete"),
                },
              ].map((item, index) => (
                <Button
                  key={index}
                  w="full"
                  bg="none"
                  borderRadius={0}
                  onClick={item.action}
                  isDisabled={item.isDisabled}
                >
                  <Flex w="full" gap={4} alignItems="center">
                    <Box fontSize="20px">
                      <item.icon
                        color={item.isDisabled ? undefined : primaryColor}
                      />
                    </Box>
                    <Text color={primaryColor}>{item.label}</Text>
                  </Flex>
                </Button>
              ))}
            </PopoverBody>
          </PopoverContent>
        </Popover>
        <Modal isOpen={isOpenConfirmModal} onClose={onCloseConfirmModal}>
          <ModalOverlay />
          <ModalContent style={{ top: "15%" }}>
            <ModalHeader>Confirm {selectedAction}</ModalHeader>
            <ModalCloseButton />
            <ModalBody>{confirmMessage}</ModalBody>
            <ModalFooter>
              <Button colorScheme="blue" mr={3} onClick={onCloseConfirmModal}>
                Close
              </Button>
              <Button variant="ghost" onClick={handleConfirmAction}>
                Confirm
              </Button>
            </ModalFooter>
          </ModalContent>
        </Modal>
        {isLoading ? (
          <GlobalLoading isLoading={isLoading} />
        ) : (
          <Modal isOpen={isOpenUpdateModal} onClose={onCloseUpdateModal}>
            <ModalOverlay />
            <ModalContent maxW="970px" overflow="hidden">
              <ModalHeader
                bgColor="#2D3748"
                color="white"
                textAlign="center"
                py={2}
                position="relative"
              >
                Update Training Program
                <Button
                  bg="none"
                  p={0}
                  color="white"
                  fontSize="25px"
                  position="absolute"
                  right={0}
                  top={1}
                  _hover={{ background: "none" }}
                  onClick={onCloseUpdateModal}
                >
                  <IoMdCloseCircleOutline />
                </Button>
              </ModalHeader>
              <ModalBody>
                <EditProgramForm
                  dataUpdate={dataUpdate}
                  handleAction={handleAction}
                  onCloseUpdateModal={onCloseUpdateModal}
                />
              </ModalBody>
            </ModalContent>
          </Modal>
        )}
      </>
    );
  }
);

export default ActionMenu;
