import * as React from "react";
import Menu from "@mui/material/Menu";
import MenuItem from "@mui/material/MenuItem";
import Fade from "@mui/material/Fade";
import ContentCopyIcon from "@mui/icons-material/ContentCopy";
import DeleteForeverIcon from "@mui/icons-material/DeleteForever";
import ModeEditOutlineIcon from "@mui/icons-material/ModeEditOutline";
import { DoplicatedClass, GetClass } from "../api/ListApi";
import { toast } from "react-toastify";
import { deleteClass, DuplicateClass } from "../api/ClassApi";
import {
  AlertDialog,
  AlertDialogBody,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogContent,
  AlertDialogOverlay,
  AlertDialogCloseButton,
  useDisclosure,
  Button,
} from "@chakra-ui/react";
import { Link } from "react-router-dom";
export default function Action({ setOpen, row, setA, setData, rerender }) {
  const [anchorEl, setAnchorEl] = React.useState(null);
  const { isOpen, onOpen, onClose } = useDisclosure();
  const cancelRef = React.useRef();
  const open1 = Boolean(anchorEl);
  const handleClick = (event) => {
    setAnchorEl(event.currentTarget);
  };
  const handleClose = () => {
    setAnchorEl(null);
  };
  const handleDelete = () => {
    onOpen();
    setAnchorEl(null);
    setOpen(true);
    setA(row);
  };
  const handleDuplicate = async () => {
    const response = await DoplicatedClass(row.classId);
    if (response.ok) {
      toast.success("Duplicate Class Successfully");
      await updateData();
    } else {
      toast.error("Duplicate Class Error");
    }
  };
  const updateData = async () => {
    const updatedList = await GetClass();
    setData(updatedList);
  };
  return (
    <div>
      <Button
        id="fade-button"
        aria-controls={open1 ? "fade-menu" : undefined}
        aria-haspopup="true"
        aria-expanded={open1 ? "true" : undefined}
        onClick={handleClick}
      >
        <span style={{ fontWeight: "900", color: "black", fontSize: "large" }}>
          ...
        </span>
      </Button>
      <Menu
        id="fade-menu"
        MenuListProps={{
          "aria-labelledby": "fade-button",
        }}
        anchorEl={anchorEl}
        open={open1}
        onClose={handleClose}
        TransitionComponent={Fade}
      >
        <Link to={`/class/${row.classId}/edit`}>
          <MenuItem onClick={handleClose} style={{ gap: "10px" }}>
            <ModeEditOutlineIcon /> Edit class
          </MenuItem>
        </Link>
        <MenuItem
          onClick={() => {
            DuplicateClass(row.classId);
            setTimeout(() => {
              rerender();
            }, 100);
          }}
          style={{ gap: "10px" }}
        >
          <ContentCopyIcon /> Duplicate class
        </MenuItem>
        <MenuItem onClick={handleDelete} style={{ gap: "10px" }}>
          <DeleteForeverIcon /> Delete class
        </MenuItem>
      </Menu>
      <AlertDialog
        isOpen={isOpen}
        leastDestructiveRef={cancelRef}
        onClose={onClose}
      >
        <AlertDialogOverlay bg="rgba(0,0,0,0.5)">
          <AlertDialogContent
            bg="white"
            top="100px"
            w="600px"
            left="500px"
            p={20}
          >
            <AlertDialogHeader fontSize="lg" fontWeight="bold">
              Delete Class
            </AlertDialogHeader>

            <AlertDialogBody>
              Are you sure? You can't undo this action afterwards.
            </AlertDialogBody>

            <AlertDialogFooter>
              <Button ref={cancelRef} onClick={onClose}>
                Cancel
              </Button>
              <Button
                color="white"
                bg="red"
                px="20px"
                py="5px"
                borderRadius="10px"
                _hover={{
                  backgroundColor: "#ff4747",
                }}
                onClick={() => {
                  onClose();
                  if (row.classStatus.toLowerCase() !== "completed") {
                    deleteClass(row.classId);
                    setTimeout(() => {
                      rerender();
                    }, 100);
                  } else {
                    toast.error("Cannot delete completed class");
                  }
                }}
                ml={3}
              >
                Delete
              </Button>
            </AlertDialogFooter>
          </AlertDialogContent>
        </AlertDialogOverlay>
      </AlertDialog>
    </div>
  );
}
