import { GetProp, UploadProps } from "antd";
import { CreateSyllabus, SingleSyllabusListResult, SyllabusDetailGeneralResult, SyllabusDetailHeaderResult, SyllabusDetailOtherResult, SyllabusDetailOutlineResult, SyllabusList, SyllabusListResult, UpdateSyllabus } from "./types";
import { toast } from "react-toastify";

export default class ApiClient {
	apiBaseUrl: string;
	private authorization: string;
	constructor() {
		// @ts-ignore
		this.apiBaseUrl = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;
		this.authorization = `Bearer ${localStorage.getItem("token")}`;
	}

	async getSyallabusList(pageNumber: number, pageSize: number) {
		let data: SyllabusListResult<SyllabusList> = {
			result: {
				data: [],
				metadata: {
					currentPage: 0,
					totalPage: 0,
					totalItems: 0,
					pageSize: 0,
					hasPrevious: false,
					hasNext: false
				}
			},
			isSuccess: false,
			message: "",
			statusCode: 0,
			title: ""
		};
		const url = `${this.apiBaseUrl}/Syllabus?PageNumber=${pageNumber}&PageSize=${pageSize}`;
		try {
			const response = await fetch(url, {
				headers: {
					Authorization: this.authorization,
				}
			});
			const responseBody = await response.json(); // Store the response body in a variable

			data = this.syllabusListConvert(responseBody);
		} catch (error) {
			console.error(error);
		}
		return data
	}

	async searchSyllabusByDate(startDate: string, endDate: string, pageNumber: number, pageSize: number) {
		let data: SyllabusListResult<SyllabusList> = {
			result: {
				data: [],
				metadata: {
					currentPage: 0,
					totalPage: 0,
					totalItems: 0,
					pageSize: 0,
					hasPrevious: false,
					hasNext: false
				}
			},
			isSuccess: false,
			message: "",
			statusCode: 0,
			title: ""
		};
		const url =
			`${this.apiBaseUrl}/date-range?` +
			new URLSearchParams({
				PageNumber: `${pageNumber}`,
				PageSize: `${pageSize}`,
				from: startDate,
				to: endDate,
			});
		try {
			const response = await fetch(url, {
				headers: {
					Authorization: this.authorization,
				}
			});
			const responseBody = await response.json();

			data = this.syllabusListConvert(responseBody);
		} catch (error) {
			console.error(error);
		}
		return data;
	}

	async searchSyllabusByKeyword(keywords: string, pageNumber: number, pageSize: number) {
		let data: SyllabusListResult<SyllabusList> = {
			result: {
				data: [],
				metadata: {
					currentPage: 0,
					totalPage: 0,
					totalItems: 0,
					pageSize: 0,
					hasPrevious: false,
					hasNext: false
				}
			},
			isSuccess: false,
			message: "",
			statusCode: 0,
			title: ""
		};

		const params = new URLSearchParams({
			keywords: keywords,
			PageNumber: `${pageNumber}`,
			PageSize: `${pageSize}`,
		});

		const url = `${this.apiBaseUrl}/search-query?${params.toString()}`;

		try {
			const response = await fetch(url, {
				headers: {
					Authorization: this.authorization,
				}
			});
			const responseBody = await response.json();

			data = this.syllabusListConvert(responseBody);
		} catch (error) {
			console.error(error);
		}

		return data;
	}


	async getSyllabusDetailHeader(syllabusId: string) {
		const url = `${this.apiBaseUrl}/header?` +
			new URLSearchParams({
				syllabusId: syllabusId,
			});
		try {
			const response = await fetch(url, {
				headers: {
					Authorization: this.authorization,
				}
			});
			const responseBody: SyllabusDetailHeaderResult = await response.json();
			responseBody.result.data.modifiedDate = new Date(responseBody.result.data.modifiedDate).toLocaleDateString();
			return responseBody;
		} catch (error) {
			console.error(error);
		}
	}

	async getSyllabusDetailGeneral(syllabusId: string) {
		const url = `${this.apiBaseUrl}/general?` +
			new URLSearchParams({
				syllabusId: syllabusId,
			});
		try {
			const response = await fetch(url, {
				headers: {
					Authorization: this.authorization,
				}
			});
			const responseBody: SyllabusDetailGeneralResult = await response.json();
			return responseBody;
		} catch (error) {
			console.error(error);
		}
	}

	async getSyllabusDetailOutline(syllabusId: string) {
		const url = `${this.apiBaseUrl}/outline?` +
			new URLSearchParams({
				syllabusId: syllabusId,
			});
		try {
			const response = await fetch(url, {
				headers: {
					Authorization: this.authorization,
				}
			});
			const responseBody: SyllabusDetailOutlineResult = await response.json();
			return responseBody;
		} catch (error) {
			console.error(error);
		}
	}

	async getSyllabusDetailOther(syllabusId: string) {
		const url = `${this.apiBaseUrl}/other?` +
			new URLSearchParams({
				syllabusId: syllabusId,
			});
		try {
			const response = await fetch(url, {
				headers: {
					Authorization: this.authorization,
				}
			});
			const responseBody: SyllabusDetailOtherResult = await response.json();
			return responseBody;
		} catch (error) {
			console.error(error);
		}
	}

	async getSyllabusDetailDeliveryType(syllabusId: string) {
		const url = `${this.apiBaseUrl}/delivery-type-percentages?` +
			new URLSearchParams({
				syllabusId: syllabusId,
			});
		try {
			const response = await fetch(url, {
				headers: {
					Authorization: this.authorization,
				}
			});
			const responseBody = await response.json(); // write model later
			return responseBody;
		} catch (error) {
			console.error(error);
		}
	}

	async importSyllaus(fileList: any[], setUploading: any, setFileList: any, duplicateHanding: string) {
		type FileType = Parameters<GetProp<UploadProps, 'beforeUpload'>>[0];

		const formData = new FormData();
		// fileList.forEach((file) => {
		// 	formData.append('files[]', file as FileType);
		// });
		formData.set('File', fileList[0] as FileType);
		formData.append('DuplicateHandling', duplicateHanding);
		setUploading(true);
		let responseBody: Response;
		// You can use any AJAX library you like
		await fetch(`${this.apiBaseUrl}/import?`, {
			method: 'POST',
			body: formData,
			headers: {
				// 'content-type': 'multipart/form-data',
				Authorization: this.authorization,
			}
		})
			.then((res) => {
				responseBody = res;
				if (res.ok) {
					setFileList([]);
					toast.success('Upload successfully.', {
						position: "top-right",
						autoClose: 5000,
						hideProgressBar: false,
						closeOnClick: true,
						pauseOnHover: true,
						draggable: true,
						progress: undefined,
						theme: "light",
					});
				}
				else {
					toast.error(`Upload failed. Code: ${res.status}`, {
						position: "top-right",
						autoClose: 5000,
						hideProgressBar: false,
						closeOnClick: true,
						pauseOnHover: true,
						draggable: true,
						progress: undefined,
						theme: "light",
					});
				}
			})
			.catch((error) => {
				toast.error(`Upload failed.`, {
					position: "top-right",
					autoClose: 5000,
					hideProgressBar: false,
					closeOnClick: true,
					pauseOnHover: true,
					draggable: true,
					progress: undefined,
					theme: "light",
				});
				return error;
			})
			.finally(() => {
				setUploading(false);
			});
		return responseBody;
	}

	async downloadSyllabusTemplate(filePath: string = 'Resources\\Templates\\Template_Import_Syllabus.xlsx'): Promise<Blob> {
		const url = `${this.apiBaseUrl}/File/download-syllabus-template?` + new URLSearchParams({
			fileUrl: filePath,
		})
		let responseBody: Promise<Blob> = Promise.resolve(new Blob());
		await fetch(url, {
			headers: {
				Authorization: this.authorization,
			}
		})
			.then((response) => {
				responseBody = response.blob();
			})
			.catch((error) => console.error(error));
		return responseBody;
	}

	async deleteSyllabus(syllabusId: string): Promise<boolean> {
		const url = `${this.apiBaseUrl}/Syllabus/?syllabusId=${syllabusId}`;
		try {
			const response = await fetch(url, {
				method: 'DELETE',
				headers: {
					Authorization: this.authorization,
				}
			});

			if (response.ok) {
				toast.success('Delete successfully.', {
					position: "top-right",
					autoClose: 5000,
					hideProgressBar: false,
					closeOnClick: true,
					pauseOnHover: true,
					draggable: true,
					progress: undefined,
					theme: "light",
				});

				return true;
			} else {
				toast.error(`Delete failed. Code: ${response.status}`, {
					position: "top-right",
					autoClose: 5000,
					hideProgressBar: false,
					closeOnClick: true,
					pauseOnHover: true,
					draggable: true,
					progress: undefined,
					theme: "light",
				});
				return false;
			}
		} catch (error) {
			toast.error('Error delete syllabus.', {
				position: "top-right",
				autoClose: 5000,
				hideProgressBar: false,
				closeOnClick: true,
				pauseOnHover: true,
				draggable: true,
				progress: undefined,
				theme: "light",
			});
			console.error(error);
			return false;
		}
	}

	async activeDeactiveSyllabus(syllabusId: string, active: boolean) {
		const url = `${this.apiBaseUrl}/active-deactive?` +
			new URLSearchParams({
				syllabusId: syllabusId,
				activate: active.toString(),
			});

		try {
			const response = await fetch(url, {
				method: 'PATCH',
				headers: {
					'Content-Type': 'application/json',
					Authorization: this.authorization,
				},
			});
			if (response.ok) {
				toast.success('Update status successfully.', {
					position: "top-right",
					autoClose: 5000,
					hideProgressBar: false,
					closeOnClick: true,
					pauseOnHover: true,
					draggable: true,
					progress: undefined,
					theme: "light",
				});
			}
			else {
				toast.error(`Update status failed. Code: ${response.status}`, {
					position: "top-right",
					autoClose: 5000,
					hideProgressBar: false,
					closeOnClick: true,
					pauseOnHover: true,
					draggable: true,
					progress: undefined,
					theme: "light",
				});
			}
		} catch (error) {
			toast.error('Error update status!', {
				position: "top-right",
				autoClose: 5000,
				hideProgressBar: false,
				closeOnClick: true,
				pauseOnHover: true,
				draggable: true,
				progress: undefined,
				theme: "light",
			});
			console.error(error);
		}
	}

	async createSyllabus(isDraft: boolean, syllabus: CreateSyllabus) {
		const url = `${this.apiBaseUrl}/Syllabus?isDraft=${isDraft}`;
		try {
			const response = await fetch(url, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
					Authorization: this.authorization,
				},
				body: JSON.stringify(syllabus),
			});

			const responseBody = await response.json();
			if (response.ok) {
				return responseBody;
			}
			else {
				toast.error(`Create syllabus failed. Code: ${response.status}`, {
					position: "top-right",
					autoClose: 5000,
					hideProgressBar: false,
					closeOnClick: true,
					pauseOnHover: true,
					draggable: true,
					progress: undefined,
					theme: "light",
				});
			}
		} catch (error) {
			toast.error('Error create syllabus.', {
				position: "top-right",
				autoClose: 5000,
				hideProgressBar: false,
				closeOnClick: true,
				pauseOnHover: true,
				draggable: true,
				progress: undefined,
				theme: "light",
			});
			console.error(error);
		}
	}

	async duplicateSyllabus(syllabusId: string, topicCode: string) {
		const url = `${this.apiBaseUrl}/duplicate`

		try {
			const response = await fetch(url, {
				method: 'POST',
				headers: {
					'Content-Type': 'application/json',
					Authorization: this.authorization,
				},
				body: JSON.stringify({ syllabusId: syllabusId, topicCode: topicCode, createdBy: localStorage.getItem("username") }),
			});

			const responseBody = await response.json() as SingleSyllabusListResult;

			if (response.ok) {
				toast.success('Duplicate syllabus successfully.', {
					position: "top-right",
					autoClose: 5000,
					hideProgressBar: false,
					closeOnClick: true,
					pauseOnHover: true,
					draggable: true,
					progress: undefined,
					theme: "light",
				});
				return responseBody;
			}
			else {
				toast.error(`Duplicate syllabus failed. Code: ${response.status}`, {
					position: "top-right",
					autoClose: 5000,
					hideProgressBar: false,
					closeOnClick: true,
					pauseOnHover: true,
					draggable: true,
					progress: undefined,
					theme: "light",
				});
				return responseBody;
			}
		} catch (error) {
			toast.error('Error duplicate syllabus.', {
				position: "top-right",
				autoClose: 5000,
				hideProgressBar: false,
				closeOnClick: true,
				pauseOnHover: true,
				draggable: true,
				progress: undefined,
				theme: "light",
			});
			console.error(error);
		}
	}

	async updateSyllabus(syllabus: UpdateSyllabus) {
		const url = `${this.apiBaseUrl}/Syllabus`
		try {
			const response = await fetch(url, {
				method: 'PUT',
				headers: {
					'Content-Type': 'application/json',
					Authorization: this.authorization,
				},
				body: JSON.stringify(syllabus),
			});
			const responseBody = await response.json() as UpdateSyllabus;

			if (response.ok) {
				toast.success('Update syllabus successfully.', {
					position: "top-right",
					autoClose: 5000,
					hideProgressBar: false,
					closeOnClick: true,
					pauseOnHover: true,
					draggable: true,
					progress: undefined,
					theme: "light",
				});
			}
			else if (response.status === 400) {
				toast.error(`One or more validation errors occurred. Code: ${response.status}`, {
					position: "top-right",
					autoClose: 5000,
					hideProgressBar: false,
					closeOnClick: true,
					pauseOnHover: true,
					draggable: true,
					progress: undefined,
					theme: "light",
				});
			}
			else {
				toast.error(`Update syllabus failed. Code: ${response.status}`, {
					position: "top-right",
					autoClose: 5000,
					hideProgressBar: false,
					closeOnClick: true,
					pauseOnHover: true,
					draggable: true,
					progress: undefined,
					theme: "light",
				});
			}
			return responseBody;
		} catch (error) {
			toast.error('Error update syllabus.', {
				position: "top-right",
				autoClose: 5000,
				hideProgressBar: false,
				closeOnClick: true,
				pauseOnHover: true,
				draggable: true,
				progress: undefined,
				theme: "light",
			});
			console.error(error);
		}
	}

	syllabusListConvert(data: SyllabusListResult<SyllabusList>) {
		data.result.data.map((item) => {
			item.id = item.syllabusId;
			item.createdDate = new Date(item.createdDate).toISOString();
			item.modifiedDate = new Date(item.modifiedDate).toISOString();
		});
		return data;
	}

	getToken() {
		this.authorization = `Bearer ${localStorage.getItem("token")}`;
	}
}
