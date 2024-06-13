import React, { Component, RefObject } from "react";
import { Button, Modal, Form, Select, Checkbox, Radio, RadioChangeEvent } from "antd";
import { EmailTemplate, TableParams } from "./NoticeTabConfigTable";
import style from "../../FilterEmailTemplate/style.module.scss";
import axios from "../../../../../axiosAuth";

// @ts-ignore
const { Option } = Select;
interface FormFilterProps {
	onClose: () => void;
	setUpdatedData: React.Dispatch<React.SetStateAction<EmailTemplate[] | undefined>>;
	tableParams: TableParams;
	setSearchParams: React.Dispatch<React.SetStateAction<Record<string, unknown>>>;
	searchParams: Record<string, unknown>;
}

// @ts-ignore
const backend_api: string = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT}`;

interface FormFilterState {
	pages: any;
	isModalOpen: boolean;
	inputValue: string;
	selectedApplyTo: string[];
	emailTemplates: EmailTemplate[];
	loading: boolean;
	page: number;
	pageSize: number | string;
	status: number | null;
	type: number[] | null;
}
const activeData = [
	{ label: "Active", value: 1 },
	{ label: "Inactive", value: 2 },
];
class FormFilter extends Component<FormFilterProps, FormFilterState> {
	private usernameRef: RefObject<HTMLInputElement>;

	constructor(props: FormFilterProps) {
		super(props);

		this.state = {
			isModalOpen: true,
			inputValue: "",
			selectedApplyTo: [],
			emailTemplates: [],
			loading: false,
			page: 1,
			pageSize: 10,
			status: null,
			type: [3],
		};
		if (this.props.searchParams.status) {
			this.setState({ status: Number(this.props.searchParams.status) });
		}
		if (this.props.searchParams.type) {
			this.setState({ type: this.props.searchParams.type as [] });
		}
		if (this.props.searchParams.applyTo) {
			this.setState({
				selectedApplyTo: this.props.searchParams.applyTo as [],
			});
		}
		this.usernameRef = React.createRef();


		this.toggleModal = this.toggleModal.bind(this);
		this.handleCancel = this.handleCancel.bind(this);
		this.handleSubmit = this.handleSubmit.bind(this);
		this.handleApplyChange = this.handleApplyChange.bind(this);
	}

	toggleModal() {
		this.setState((prevState) => ({
			isModalOpen: !prevState.isModalOpen,
		}));
		this.props.onClose();
	}

	handleCancel() {
		this.props.setSearchParams({});
		this.toggleModal();
	}

	handleSubmit() {
		// Grabbing the option
		const { selectedApplyTo, type, status } = this.state;

		let url = `${backend_api}/api/emailTemplates/list?page=${this.props.tableParams.pagination?.current ? this.props.tableParams.pagination?.current : 1}&pageSize=${this.props.tableParams.pagination?.pageSize}`;

		// Provide option
		if (type !== null && type.length > 0) url += `&type=${type.join(",")}`;
		if (status !== null) url += `&status=${status}`;
		if (selectedApplyTo.length > 0) url += `&applyTo=${selectedApplyTo.join(",")}`;

		// Send request
		axios.get(url).then((resp) => {
			this.props.setUpdatedData(resp.data);
			this.props.pages.setTotalPage(resp.data.length);
			// Close the form
			this.toggleModal();
		});
	}

	handleApplyChange(e: RadioChangeEvent) {
		const value = e.target.value;
		this.setState({
			selectedApplyTo: [value],
		});
	}

	render() {
		return (
			<>
				<Modal open={this.state.isModalOpen} onCancel={this.toggleModal} footer={null} closable={false} className={style.popup}>
					<Form onFinish={this.handleSubmit} className={style.modalBody}>
						<Form.Item label={<span className={style.tag}>Status</span>} className={style.select}>
							<Select placeholder="Select one" value={this.state.status !== null ? String(this.state.status) : (activeData.find((item) => item.value == this.props.searchParams.status)?.label as string)} onChange={(value) => this.setState({ status: value ? parseInt(value) : null })}>
								<Option value="1">Active</Option>
								<Option value="2">Inactive</Option>
							</Select>
						</Form.Item>

						<Form.Item label={<span className={style.tag}>Categories</span>} className={style.form_group}>
							<Checkbox.Group onChange={(values) => this.setState({ type: values })} value={this.state.type || (this.props.searchParams.type as [3])}>
								<label>Notice</label>
							</Checkbox.Group>
						</Form.Item>

						<Form.Item label={<span className={style.tag}>Apply To</span>}>
							<Radio.Group onChange={this.handleApplyChange} value={this.state.selectedApplyTo.length > 0 ? this.state.selectedApplyTo[0] : undefined}>
								<Radio value="Trainer">Trainer</Radio>
								<Radio value="Student">Student</Radio>
							</Radio.Group>
						</Form.Item>

						<Form.Item>
							<div className={style.modal_footer}>
								<Button type="default" onClick={this.handleCancel} className={style.clear}>
									Clear
								</Button>
								<Button type="primary" htmlType="submit" className={style.submit}>
									Save
								</Button>
							</div>
						</Form.Item>
					</Form>
				</Modal>
			</>
		);
	}
}
export default FormFilter;
