import { useState, useEffect, MouseEventHandler } from "react";
import style from "./style.module.scss";
import { IoMdCloseCircleOutline } from "react-icons/io";
import { Modal, Button, Form, Input, Select } from "antd";
import { css } from "@emotion/css";
import { toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import { useContext } from "react";
import axios from "../../../../axiosAuth";
import { emailContext } from "@/pages/EmailConfigurationPage";

// @ts-ignore
const backend_api: string = `${import.meta.env.VITE_API_HOST}:${
  import.meta.env.VITE_API_PORT
}`;

const Index = ({ onClose }) => {
  const context = useContext(emailContext);
  const [isModalOpen, setModalOpen] = useState(true);
  const [inputValue, setInputValue] = useState("");
  const [nameValue, setNameValue] = useState("");
  const [descriptionValue, setDescriptionValue] = useState("");
  const [canSubmit, setCanSubmit] = useState(true);
  const [isCreating, setIsCreating] = useState(false);

  const [nameError, setNameError] = useState(false);
  const [categoriesError, setCategoriesError] = useState(false);
  const [applyToError, setApplyToError] = useState(false);
  const [descriptionError, setDescriptionError] = useState(false);

  const toggleModal = () => {
    setModalOpen(!isModalOpen);
    onClose();
  };

  const handleCancel = () => {
    toggleModal();
  };
  const { Option } = Select;

  const handleCategoryChange = (value: string) => {
    setAdd({ ...add, categories: value });
    setCategoriesError(false);
  };

  const handleApplyToChange = (value: string) => {
    setAdd({ ...add, applyTo: value });
    setApplyToError(false);
  };

  const handleSubmit: MouseEventHandler<HTMLElement> = async (event) => {
    event.preventDefault();

    if (!canSubmit || isCreating) {
      return; // Không cho phép tạo mới nếu chưa đủ thời gian hoặc đang trong quá trình tạo mới
    }
    if (!add.name || !add.categories || !add.applyTo || !add.description) {
      setNameError(!add.name);
      setCategoriesError(!add.categories);
      setApplyToError(!add.applyTo);
      setDescriptionError(!add.description);
      return;
    }

    try {
      const orderedAdd = {
        name: add.name,
        description: add.description,
        type: add.categories,
        applyTo: add.applyTo,
      };
      setIsCreating(true); // Bắt đầu quá trình tạo mới
      const response = await axios.post(
        `${backend_api}/api/emailTemplates/add`,
        orderedAdd
      );
      setAdd(response.data);
      toggleModal();
      toast.success("Create email template successful");
      setIsCreating(false);

      // Refresh
      context.setRefresh(context.refresh + 1);

      setCanSubmit(true);
    } catch (error) {
      setIsCreating(false);
    }
  };

  const [add, setAdd] = useState({
    name: "",
    categories: "",
    applyTo: "",
    description: "",
  });

  useEffect(() => {
  }, [add]);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setNameValue(value);

    setAdd((prevAdd) => ({
      ...prevAdd,
      [name]: value,
    }));
    if (name === "name") setNameError(false);
    else if (name === "categories") setCategoriesError(false);
    else if (name === "applyTo") setApplyToError(false);
    else if (name === "description") setDescriptionError(false);
  };

  const handleTextAreaChange = (e: React.ChangeEvent<HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setDescriptionValue(value);
    setInputValue(value);
    setAdd((prevAdd) => ({
      ...prevAdd,
      [name]: value,
    }));
    setDescriptionError(false);
  };

  const modalStyle = css`
    .ant-modal-content {
      padding: 0;
    }
    .ant-modal-close {
      background: none;
      padding-bottom: 10px;
    }
    .ant-btn-default {
      border-radius: 16px;
      color: #2d3748;
      background: none;
    }
    .ant-btn-default:hover {
      background-color: transparent;
    }
    .ant-btn-default {
      border-radius: 16px;
      color: #2d3748;
      background: none;
    }
    .ant-modal-footer {
      text-align: center;
    }
  `;
  return (
    <Modal
      open={isModalOpen}
      title={<div className={style.titleStyle}>Create email template list</div>}
      className={modalStyle}
      onOk={handleSubmit}
      onCancel={handleCancel}
      closeIcon={
        <div style={{ background: "none" }}>
          <IoMdCloseCircleOutline
            style={{ fontSize: "20px", color: "white" }}
          />
        </div>
      }
      footer={[]}
    >
      <div className={style.bodyModal}>
        <Form name="basic">
          <Form.Item
            validateStatus={nameError ? "error" : ""}
            help={
              nameError ? (
                <span className={style.errorText}>
                  Please enter name template
                </span>
              ) : (
                ""
              )
            }
          >
            <div style={{ fontWeight: "bold", paddingBottom: "6px" }}>
              Name Template
            </div>
            <Input name="name" onChange={handleInputChange} value={nameValue} />
          </Form.Item>
          <Form.Item
            validateStatus={categoriesError ? "error" : ""}
            help={
              categoriesError ? (
                <span className={style.errorText}>
                  Please select a category
                </span>
              ) : (
                ""
              )
            }
          >
            <div style={{ fontWeight: "bold", paddingBottom: "6px" }}>
              Categories
            </div>
            <Select
              onChange={handleCategoryChange}
              placeholder="Select one"
              id="categories"
            >
              <Option value="1">Reserve</Option>
              <Option value="2">Remind</Option>
              <Option value="3">Notice</Option>
              <Option value="4">Other</Option>
            </Select>
          </Form.Item>
          <Form.Item
            validateStatus={applyToError ? "error" : ""}
            help={
              applyToError ? (
                <span className={style.errorText}>Please select apply to</span>
              ) : (
                ""
              )
            }
            style={{ width: "100%" }}
          >
            <div style={{ fontWeight: "bold", paddingBottom: "6px" }}>
              Apply to
            </div>
            <Select
              placeholder="Select one"
              id="applyTo"
              onChange={handleApplyToChange}
            >
              <Option value="Student">Student</Option>
              <Option value="Trainer">Trainer</Option>
            </Select>
          </Form.Item>
          <Form.Item
            validateStatus={descriptionError ? "error" : ""}
            help={
              descriptionError ? (
                <span className={style.errorText}>
                  Please enter description
                </span>
              ) : (
                ""
              )
            }
          >
            <div style={{ fontWeight: "bold", paddingBottom: "6px" }}>
              Description
            </div>
            <Input.TextArea
              placeholder="Enter the description"
              maxLength={50}
              value={descriptionValue}
              onChange={handleTextAreaChange}
              name="description"
            />
            <div style={{ float: "right" }}>{inputValue.length}/50</div>
          </Form.Item>
        </Form>
      </div>

      <div className={style.footer}>
        <Button type="text" className={style.cancelbtn} onClick={handleCancel}>
          Cancel
        </Button>
        <div>
          <Button
            type="primary"
            className={style.createbtn}
            onClick={handleSubmit}
          >
            Create
          </Button>
        </div>
      </div>
    </Modal>
  );
};

export default Index;
