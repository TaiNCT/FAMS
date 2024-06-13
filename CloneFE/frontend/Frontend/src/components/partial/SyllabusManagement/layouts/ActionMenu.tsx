import {
  AlertDialog,
  AlertDialogBody,
  AlertDialogContent,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogOverlay,
  Box,
  Button,
  Flex,
  Text,
  useDisclosure,
} from "@chakra-ui/react";
import { IoAddCircleOutline } from "react-icons/io5";
import { LuCopy, LuPencil } from "react-icons/lu";
import { MdOutlineDeleteForever } from "react-icons/md";
import React from "react";
import { Link, useNavigate } from "react-router-dom";

const ActionMenu = ({
  params,
  onConfirmDelete,
  handleDuplicate,
  status,
}: {
  params: any;
  onConfirmDelete: any;
  handleDuplicate: any;
  status: string;
}) => {
  const { onClose, onOpen, isOpen } = useDisclosure();
  const cancelRef = React.useRef();
  let syllabusStatus: boolean = false;
  const navigate = useNavigate();

  if (status.toLowerCase() !== 'active') {
    syllabusStatus = false;
  }
  else {
    syllabusStatus = true;
  }

  return (
    <>
      <Button w="full" bg="none" borderRadius={0} onClick={() => { navigate('/trainingprogram') }}>
        <Flex w="full" gap={4} alignItems="center">
          <Box fontSize="20px">
            <IoAddCircleOutline height="27px" color="#285D9A" />{" "}
          </Box>
          <Text color="blue.700">Add training program</Text>
        </Flex>
      </Button>
      <Link to={`/syllabus/edit/${params}`}>
        <Button w="full" bg="none" borderRadius={0}>
          <Flex w="full" gap={4} alignItems="center">
            <Box fontSize="20px">
              <LuPencil height="24px" color="#285D9A" />
            </Box>
            <Text color="blue.700">Edit syllabus</Text>
          </Flex>
        </Button>
      </Link>
      <Button w="full" bg="none" borderRadius={0} onClick={handleDuplicate}>
        <Flex w="full" gap={4} alignItems="center">
          <Box fontSize="20px">
            <LuCopy color="#285D9A" />
          </Box>
          <Text color="blue.700">Duplicate syllabus</Text>
        </Flex>
      </Button>
      <Button
        w="full"
        bg="none"
        borderRadius={0}
        isDisabled={syllabusStatus}
        disabled={syllabusStatus}
        onClick={() => {
          onOpen();
        }}
      >
        <Flex w="full" gap={4} alignItems="center">
          <Box fontSize="20px">
            <MdOutlineDeleteForever color="#285D9A" />
          </Box>
          <Text color="blue.700">Delete syllabus</Text>
        </Flex>
      </Button>
      <AlertDialog
        isOpen={isOpen}
        leastDestructiveRef={cancelRef}
        onClose={onClose}
      >
        <AlertDialogOverlay>
          <AlertDialogContent>
            <AlertDialogHeader fontSize="lg" fontWeight="bold">
              Delete Syllabus
            </AlertDialogHeader>

            <AlertDialogBody>
              Are you sure? You can't undo this action afterwards.
            </AlertDialogBody>

            <AlertDialogFooter>
              <Button ref={cancelRef} onClick={onClose}>
                Cancel
              </Button>
              <Button
                colorScheme="red"
                onClick={() => {
                  onClose();
                  onConfirmDelete(params);
                }}
                ml={3}
              >
                Delete
              </Button>
            </AlertDialogFooter>
          </AlertDialogContent>
        </AlertDialogOverlay>
      </AlertDialog>
    </>
  );
};

export default ActionMenu;
