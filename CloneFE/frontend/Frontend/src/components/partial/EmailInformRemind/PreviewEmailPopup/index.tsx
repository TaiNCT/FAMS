import { Modal, Button } from "antd";
import React, { useState } from "react";
import { IoMdCloseCircleOutline } from "react-icons/io";
import style from "./style.module.scss";
//import { css } from "@emotion/css";

function PreviewEmailPopUp({ setopen }) {
	const [previewVisible, setPreviewVisible] = useState(false);

	const showPreviewModal = () => {
		setopen(false);
		setPreviewVisible(true);
	};

	const handleOk = () => {
		setPreviewVisible(false);
	};

	const handleCancel = () => {
		setPreviewVisible(false);
	};

  // const modalStyle = css`
  //   .ant-modal-content {
  //     padding: 0;
  //     width: 600px;
  //   }
  //   .ant-modal-close {
  //     background: none;
  //     padding-bottom: 10px;
  //   }
  //   .ant-btn-default {
  //     border-radius: 16px;
  //     color: #2d3748;
  //     background: none;
  //   }
  //   .ant-btn-default:hover {
  //     background-color: transparent;
  //   }
  //   .ant-btn-default {
  //     border-radius: 16px;
  //     color: #2d3748;
  //     background: none;
  //   }
  //   .ant-modal-footer {
  //     text-align: center;
  //   }
  //   ..ant-btn:hover,
  //   .ant-btn:focus {
  //   }
  // `;

  return (
    <div>
      <Button onClick={showPreviewModal} className={style.previewbtn}>
        Preview
      </Button>
      <Modal
        title={<div className={style.titleStyle}>Preview email template</div>}
       // className={modalStyle}
        // onOk={handleOk}
        onCancel={handleCancel}
        visible={previewVisible}
        closeIcon={
          <div style={{ background: "none" }}>
            <IoMdCloseCircleOutline
              style={{ fontSize: "20px", color: "white" }}
            />
          </div>
        }
        footer={[]}
      >
        <div className={style.content}>
          <table>
            <div style={{ padding: "6px 0" }}>
              <tr>
                <th style={{ textAlign: "left", width: "110px" }}>
                  Template name
                </th>
                <td style={{ fontWeight: "normal" }}>Nhắc gởi điểm</td>
              </tr>
            </div>
            <div style={{ padding: "6px 0" }}>
              <tr>
                <th style={{ textAlign: "left", width: "110px" }}>From</th>
                <td style={{ fontWeight: "normal" }}>Vi Vi (vivi@gmail.com)</td>
              </tr>
            </div>
            <div style={{ padding: "6px 0" }}>
              <tr>
                <th style={{ textAlign: "left", width: "110px" }}>To</th>
                <td style={{ fontWeight: "normal" }}>TrungDVQ@fsoft.com.vn</td>
              </tr>
            </div>
            <div style={{ padding: "6px 0" }}>
              <tr>
                <th style={{ textAlign: "left", width: "110px" }}>Cc</th>
                <td style={{ fontWeight: "normal" }}>vivi@gmail.com</td>
              </tr>
            </div>
            <div style={{ padding: "6px 0" }}>
              <tr>
                <th style={{ textAlign: "left", width: "110px" }}>Subject</th>
                <td style={{ fontWeight: "normal" }}>Lorem ipsum</td>
              </tr>
            </div>
            <div style={{ padding: "6px 0" }}>
              <tr>
                <th style={{ textAlign: "left", width: "110px" }}>Body</th>
                <td style={{ fontWeight: "normal" }}>
                  Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed
                  do eiusmod tempor incididunt ut labore et dolore magna aliqua.
                  Enim blandit volutpat maecenas volutpat blandit aliquam etiam
                  erat velit. Lorem ipsum dolor sit amet, consectetur adipiscing
                  elit, sed do eiusmod tempor incididunt ut labore et dolore
                  magna aliqua. Dignissim convallis aenean et tortor.
                  Sollicitudin ac orci phasellus egestas tellus.
                  ----------------- FAM Admin
                </td>
              </tr>
            </div>
          </table>
        </div>
        <div className={style.footer}>
          <Button
            type="text"
            className={style.cancelbtn}
            onClick={handleCancel}
          >
            Back
          </Button>
          <Button type="text" className={style.previewbtn} onClick={handleOk}>
            Send
          </Button>
        </div>
      </Modal>
    </div>
  );
}

export default PreviewEmailPopUp;
