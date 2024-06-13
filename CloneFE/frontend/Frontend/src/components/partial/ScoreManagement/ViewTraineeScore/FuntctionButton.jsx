// ImportButton component
import { Button, Modal, ModalOverlay, ModalContent, ModalBody, useDisclosure, Flex } from "@chakra-ui/react";
import { UploadPage } from "../UploadPage/index";
import { ChakraProvider } from "@chakra-ui/react";
import { motion } from "framer-motion";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { MdFileDownload, MdFileUpload } from "react-icons/md";
import style from "./style.module.scss"; // Make sure to import your styles
import { Text, Box } from "@chakra-ui/react";
import axios from "@/axiosAuth";
import { LoadingButton } from "@mui/lab";

const backend_api = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;

const FunctionButton = ({ onUpdateData, onBlurToggle }) => {
	const { isOpen, onOpen, onClose } = useDisclosure();
	const { classId } = useParams();
	const [isload, setIsload] = useState(null);

	useEffect(() => {
		onBlurToggle(isOpen);
	}, [isOpen, onBlurToggle]);

	// Put export function here
	const onExportClick = () => {
		setIsload(true);

		axios({
			url: `${backend_api}/api/Export/get/${classId}`,
			method: "GET",
			responseType: "blob",
		})
			.then((resp) => {
				// create file link in browser's memory
				const href = URL.createObjectURL(resp.data);

				// create "a" HTML element with href to file & click
				const link = document.createElement("a");
				link.href = href;
				link.setAttribute("download", "score.xlsx"); //or any other extension
				document.body.appendChild(link);
				link.click();

				// clean up "a" element & remove ObjectURL
				document.body.removeChild(link);
				URL.revokeObjectURL(href);

				setIsload(false);
			})
			.catch((e) => {
				setIsload(false);
			});
	};

	return (
		<>
			<Flex justifyContent="flex-end" gap={25} mr={0}>
				<Button
					onClick={onOpen}
					bg="#4db84d"
					color="white"
					px={16}
					py={8}
					display="flex"
					borderRadius="5px"
					alignItems="center"
					justifyContent="center"
					_focus={{
						boxShadow: "none",
					}}
					transition="background-color 0.3s"
					_hover={{
						opacity: 0.9,
					}}
				>
					<div style={{ display: "flex", alignItems: "center", gap: "10px" }}>
						<Box as={MdFileUpload} boxSize="24px" />

						<Text fontSize="large">Import</Text>
					</div>
				</Button>

				<Button
					onClick={onExportClick}
					bg="#D45B13"
					color="white"
					px={16}
					display="flex"
					borderRadius="5px"
					alignItems="center"
					justifyContent="center"
					_focus={{
						boxShadow: "none",
					}}
					transition="background-color 0.3s"
					_hover={{
						opacity: 0.9,
					}}
				>
					<div style={{ display: "flex", alignItems: "center", gap: "10px" }}>
						{isload && <LoadingButton className={style.loader} loading={true}></LoadingButton>}
						{!isload && (
							<>
								<Box as={MdFileDownload} boxSize="24px" />
								<Text fontSize="large">Export</Text>
							</>
						)}
					</div>
				</Button>
			</Flex>
				<Modal isOpen={isOpen} onClose={onClose}>
					<ModalOverlay className={style.modalOverlayBlur} /> {/* Apply the blur effect here */}
					<motion.div>
						<ModalContent>
							<ModalBody>
								<UploadPage onClose={onClose} onUpdateData={onUpdateData} />
							</ModalBody>
						</ModalContent>
					</motion.div>
				</Modal>
		</>
	);
};

export default FunctionButton;
