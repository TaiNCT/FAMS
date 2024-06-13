import { Modal, Button, Select } from "antd";
import React, { useState } from "react";
import { IoMdCloseCircleOutline } from "react-icons/io";
import style from "./style.module.scss";
//import { css } from "@emotion/css";
import PreviewEmailPopUp from "../PreviewEmailPopup/";

export default function SendEmailPopUp({ open, setopen }) {
  const handleChange = (value: string) => {
  };

  // const modalStyle = css`
  //   .ant-modal-content {
  //     padding: 0;
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

  // const handleSendEmailCancel = () => {
  //   setSendEmailVisible(false);
  // };

  // const showPreviewModal = () => {
  //   setSendEmailVisible(false);
  // };

  // const modalStyle = css`
  //   .ant-modal-content {
  //     padding: 0;
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
      {/* <Button onClick={showSendEmailModal}>Open Modal</Button> */}
      <Modal
        open={open}
        title={<div className={style.titleStyle}>Select email template</div>}
        //className={modalStyle}
        // onOk={showPreviewModal}
        // onCancel={handleSendEmailCancel}
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
                  Categories
                </th>
                <td style={{ fontWeight: "normal" }}>Reserve</td>
              </tr>
            </div>
            <div style={{ padding: "6px 0" }}>
              <tr>
                <th style={{ textAlign: "left", width: "110px" }}>Apply to</th>
                <td style={{ fontWeight: "normal" }}>Trainer</td>
              </tr>
            </div>
            <div style={{ padding: "6px 0" }}>
              <tr>
                <th style={{ textAlign: "left", width: "110px" }}>Send to</th>
                <td style={{ fontWeight: "normal" }}>TrungDVQ@fsoft.com.vn</td>
              </tr>
            </div>
            <div style={{ padding: "6px 0" }}>
              <tr>
                <th style={{ textAlign: "left", width: "110px" }}>
                  Template name
                </th>
                <td style={{ fontWeight: "normal" }}>
                  <Select
                    placeholder="Select one"
                    style={{ width: 280 }}
                    onChange={handleChange}
                    options={[
                      { value: "1", label: "dsadasdasd" },
                      { value: "2", label: "Lusadsscy" },
                      { value: "3", label: "sdadasd" },
                    ]}
                  />
                </td>
              </tr>
            </div>
          </table>
        </div>
        <div className={style.footer}>
          <Button
            type="text"
            className={style.cancelbtn}
            onClick={()=>setopen(false)}
          >
            Cancel
          </Button>
          <div>
            <PreviewEmailPopUp setopen={setopen} />
          </div>
        </div>
      </Modal>
    </div>
  );
}
