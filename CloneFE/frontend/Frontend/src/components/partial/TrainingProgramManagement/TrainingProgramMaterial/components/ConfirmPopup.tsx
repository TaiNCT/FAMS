import {
  AlertDialog,
  AlertDialogBody,
  AlertDialogContent,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogOverlay,
  Button,
} from "@chakra-ui/react";
import React from "react";
import { useRef } from "react";

interface ConfirmPopupProps {
  open: boolean;
  setOpen: (c: boolean) => void;
  fileName: string;
  handleDelete: (fileName: string) => void;
}
const ConfirmPopup: React.FC<ConfirmPopupProps> = (props) => {
  const cancelRef = useRef<HTMLButtonElement>(null);

  const handleClose = () => {
    props.setOpen(!open);
  };

  return (
    <React.Fragment>
      <AlertDialog
        isOpen={props.open}
        // leastDestructiveRef={cancelRef}
        onClose={() => handleClose()}
        leastDestructiveRef={cancelRef}
      >
        <AlertDialogOverlay>
          <AlertDialogContent>
            <AlertDialogHeader fontSize="lg" fontWeight="bold">
              Delete material
            </AlertDialogHeader>

            <AlertDialogBody>
              <span style={{ fontWeight: "500" }}>Filename: </span>
              <b style={{ color: "rgb(19, 106, 206)" }}>{props.fileName}</b>
            </AlertDialogBody>

            <AlertDialogFooter>
              <Button ref={cancelRef} onClick={handleClose}>
                Cancel
              </Button>
              <Button
                colorScheme="red"
                onClick={() => {
                  handleClose();
                  props.handleDelete(props.fileName);
                }}
                ml={3}
              >
                Delete
              </Button>
            </AlertDialogFooter>
          </AlertDialogContent>
        </AlertDialogOverlay>
      </AlertDialog>
    </React.Fragment>
  );
};

export default ConfirmPopup;
