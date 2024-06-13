import {
    Button,
    Modal,  
    ModalOverlay,
    ModalContent,
    ModalHeader,
    ModalCloseButton,
    ModalBody,
    useDisclosure,
  } from "@chakra-ui/react";
  import ImportPopup from "./ImportNewRolePopup";
  import { CiImport } from "react-icons/ci";
  
  const ImportButton = ({ onUpdateData }) => {
    const { isOpen, onOpen, onClose } = useDisclosure();
  
    return (
      <>
        <Button
          onClick={onOpen}
          bg="#2f913f"
          color="white"
          px={4}
          py={2}
          display="flex"
          alignItems="center"
          justifyContent="center"
          _hover={{
            bg: "#4A5568",
          }}
          _active={{
            bg: "#2f913f",
          }}
          _focus={{
            boxShadow: "none",
          }}
          transition="background-color 0.3s"
        >
          <div style={{ display: "flex", alignItems: "center", gap: "4px" }}>
            <CiImport style={{ fontSize: "1.3em" }} />
            <span>Import New Role</span>
          </div>
        </Button>
        <Modal isOpen={isOpen} onClose={onClose}>
          <ModalOverlay />
          <ModalContent>
            <ModalHeader>Import Excel File</ModalHeader>
            <ModalCloseButton />
            <ModalBody>
              <ImportPopup onCancel={onClose} onUpdateData={onUpdateData} />
            </ModalBody>
          </ModalContent>
        </Modal>
      </>
    );
  };
  
  export default ImportButton;
  