import style from "./style.module.scss";
import Navbar from "../../components/layouts/Navbar/index";
import Sidebar from "../../components/layouts/Sidebar";
import Footer from "../../components/layouts/Footer";
import { Switch } from "antd";
import Checkbox from "@mui/material/Checkbox";
import { Link, useParams, useNavigate } from "react-router-dom";
import { Select, Form } from "antd";
import { css } from "@emotion/css";
import ReactQuill from "react-quill";
import "react-quill/dist/quill.snow.css";
import { useState, useEffect, createRef, useRef } from "react";
import axios from "../../axiosAuth";
import { format } from "date-fns";
import { toast } from "react-toastify";
import CircularProgress from "@mui/material/CircularProgress";
import Box from "@mui/material/Box";

// @ts-ignore
const backend_api: string = `${import.meta.env.VITE_API_HOST}:${
  import.meta.env.VITE_API_PORT
}`;

function DetailPage({ onUpdateES }) {
  const { temId } = useParams();
  const [subject, setSubject] = useState("");
  const [templateDetails, setTemplateDetails] = useState({});
  const [error, setError] = useState(null);
  const [emailName, setEmailName] = useState("");
  const [description, setDescription] = useState("");
  const [createdDate, setCreatedDate] = useState("");
  const [subjectError, setSubjectError] = useState("");
  const [categories, setCategories] = useState("");
  const [roleUser, setRoleUser] = useState([]);
  const [templatedata, setTemplatedata] = useState({});
  const [isSavingStatus, setIsSavingStatus] = useState(false);
  const navigate = useNavigate();

  // Later on, all input elements will use this refs to send stuffs
  const UseDearName = createRef();
  const AttendantRef = createRef();
  const QuickScoreRef = createRef();
  const AuditScoreRef = createRef();
  const PracticeScoreRef = createRef();
  const GPARef = createRef();
  const FinalStatusRef = createRef();
  const subjectRef = createRef();
  const bodyRef = createRef();
  const activeRef = createRef();
  const [sender, setSender] = useState(null);

  // All of the following values have to be "true" for the UI to completely load
  const [finish1, setFinished1] = useState(false);
  const [finish2, setFinished2] = useState(false);

  const modules = {
    toolbar: [
      [{ header: [1, 2, 3, 4, 5, 6, false] }],
      ["bold", "italic", "underline", "strike", "blockquote"],
      [{ size: [] }],
      [{ font: [] }],
      [{ align: ["right", "center", "justify"] }],
      [{ list: "ordered" }, { list: "bullet" }],
      ["link"],
      [{ color: ["red", "black"] }],
    ],
  };
  const modalStyle = css`
    .ant-select-selector {
      border: 2px solid #8b8b8b !important;
      padding: 18px !important;
      border-radius: 10px;
    }
    .ant-select-arrow {
      padding-top: 10px;
    }
  `;

  const mapCategories = (categories) => {
    switch (categories) {
      case 1:
        return "Reserve";
      case 2:
        return "Remind";
      case 3:
        return "Notice";
      case 4:
        return "Other";
      default:
        return "";
    }
  };

  // Call API to get data
  useEffect(() => {
    const callapi = async () => {
      try {
        let resp = await axios.get(`${backend_api}/api/template/get/${temId}`);
        let data = JSON.parse(resp.data.content);
        setTemplatedata(data);
        setSender(data.from);
        setFinished2(true);
      } catch (e) {
        if (e.response.status === 404) {
          // Unable to find this specific ID

        }
      }
    };

    callapi();

    axios
      .get(`${backend_api}/api/emailsend/preview/${temId}`)
      .then((response) => {
        setTemplateDetails(response.data);
        setEmailName(response.data.emailName);
        setDescription(response.data.description);
        setCategories(response.data.receiverType);
        setCreatedDate(
          format(new Date(response.data.createdDate), "dd/MM/yyyy")
        );

        // Getting a list of user based on the current role
        axios
          .post(`${backend_api}/api/EmailGetUser/get/${response.data.role}`)
          .then((resp) => {
            setRoleUser(resp.data);
            setFinished1(true);
          })
          .catch((e) => {});
      })
      .catch((error) => setError(error.message));
  }, []);

  // API Change Status
  const handleStatusChange = async (newStatus) => {
    setIsSavingStatus(true);

    try {
      let resp = await axios.put(`${backend_api}/api/emailsend/change-status`, {
        EmailTemplateId: temId,
        Status: newStatus,
      });
      toast.success("Status changed successfully");
    } catch (e) {
      console.error(error);
      toast.error("Failed to change status");
    }

    setIsSavingStatus(false);
  };

  // API Create Email Template Content
  const handleSave = () => {
    // Validation goes here
    const subjecttext = subjectRef.current.value.trim();
    setSubjectError(subjecttext ? "" : "This field is required");
    if (!subjecttext) return;

    const bodyContent = JSON.stringify({
      from: sender,
      isactive: activeRef.current.ariaChecked !== "false",
      subject: subjectRef.current.value.trim(),
      body: bodyRef.current.value,
      isdearname: UseDearName.current.querySelector("input").checked,
      attendscore: AttendantRef.current.querySelector("input").checked,
      isaudit: AuditScoreRef.current.querySelector("input").checked,
      isgpa: GPARef.current.querySelector("input").checked,
      finalstatus: FinalStatusRef.current.querySelector("input").checked,
    });

    // Edit
    axios
      .post(`${backend_api}/api/emailsend/edit`, {
        templateId: temId,
        content: bodyContent,
      })
      .then((resp) => {
        if (resp.status > 200) {
          // Success
          toast.success("Successfully updated template.");
        }
      })
      .catch((e) => {
      });
  };

  return (
    <div className={`${style.main} enable-scroll`}>
      <Navbar />
      <div className={style.maincontent}>
        <Sidebar />
        <div className={style.header}>
          {(!finish1 || !finish2) && (
            <Box
              sx={{ display: "flex", justifyContent: "center", height: "100%" }}
            >
              <CircularProgress
                style={{
                  margin: "auto",
                }}
              />
            </Box>
          )}
          {finish1 && finish2 && (
            <Form>
              <div
                className={style.headercontent}
                style={{
                  padding: "16px 32px",
                  fontSize: "32px",
                  letterSpacing: "6px",
                }}
              >
                Template details
              </div>

              {/* Template Details Part*/}
              <div className={style.templatedetailcontent}>
                <div className={style.leftcolumm}>
                  <table>
                    <tr>
                      <th>Email name</th>
                      <td>{emailName}</td>
                    </tr>

                    <tr>
                      <th>Description</th>
                      <td>{description}</td>
                    </tr>

                    <tr>
                      <th>Categories</th>
                      <td>{mapCategories(categories)}</td>
                    </tr>
                  </table>
                </div>

                <div className={style.rightcolumm}>
                  <table>
                    <tr>
                      <th>Apply to</th>
                      <td>{templateDetails.role}</td>
                    </tr>

                    <tr>
                      <th>Created on</th>
                      <td>{createdDate}</td>
                    </tr>
                    <tr>
                      <th>Active</th>
                      <td>
                        <Switch
                          ref={activeRef}
                          loading={isSavingStatus}
                          defaultChecked={templatedata.isactive} // Assuming 1 is active status
                          onChange={
                            (checked) => {
                              handleStatusChange(checked ? 1 : 2);
                            } // Assuming 1 is active status and 2 is inactive status
                          }
                          checkedChildren={
                            <span style={{ color: "limegreen" }}></span>
                          } // Change the button color to orange when checked
                          unCheckedChildren={
                            <span style={{ color: "gray" }}></span>
                          } // Change the button color to gray when unchecked
                        />
                      </td>
                    </tr>
                  </table>
                </div>
              </div>

              {/* Sender Part */}
              <div className={style.sender}>
                <div className={style.sendercontent}>Sender</div>
                <div className={style.input}>
                  <div className={style.frominput}>
                    <label className={style.contenttext}>From</label>
                    <Select
                      defaultValue={sender}
                      onChange={(e) => setSender(e)}
                      style={{
                        width: "78%",
                        margin: "26px 0 0 12px",
                      }}
                      className={modalStyle}
                      placeholder="Select one"
                      options={roleUser.map((e) => {
                        return {
                          value: e.id,
                          label: e.username,
                        };
                      })}
                    />
                  </div>
                </div>
              </div>

              {/* Content Part */}
              <div className={style.content}>
                <div className={style.Contentcontent}>Content</div>
                <div>
                  <div className={style.input}>
                    <div className={style.fromgroup}>
                      {/* Subject */}
                      <div
                        style={{ width: "100%", position: "relative" }}
                        className={style.frominput}
                      >
                        <label className={style.contenttextSubject}>
                          Subject
                        </label>
                        <input
                          ref={subjectRef}
                          placeholder="Email subject"
                          type="text"
                          name="name"
                          className={style.input}
                          defaultValue={templatedata.subject}
                          onChange={(e) => {
                            setSubject(e.target.value);
                            if (!e.target.value) {
                              setSubjectError("This field is required");
                            } else {
                              setSubjectError("");
                            }
                          }}
                        />
                        {subjectError && (
                          <span
                            style={{
                              position: "absolute",
                              bottom: "-5px",
                              left: 240,
                              fontStyle: "italic",
                              fontSize: "0.9em",
                              color: "red",
                            }}
                          >
                            {subjectError}
                          </span>
                        )}
                      </div>

                      {/* Use dear name */}
                      <div
                        style={{ width: "100%" }}
                        className={style.frominput}
                      >
                        <label className={style.contenttextUseDearName}>
                          Use dear name
                        </label>
                        <div className={style.checkbox}>
                          <Checkbox
                            ref={UseDearName}
                            defaultChecked={templatedata.isdearname}
                            color="default"
                          />
                        </div>
                      </div>

                      {/*Body Content*/}
                      <div
                        style={{ width: "100%" }}
                        className={style.frominput}
                      >
                        <label className={style.contenttextBody}>Body</label>
                        <ReactQuill
                          ref={bodyRef}
                          theme="snow"
                          className={style.editorInput}
                          modules={modules}
                          defaultValue={templatedata.body}
                        />
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              {/* Score Part */}
              <div className={style.sender}>
                <div className={style.sendercontent}>Score</div>
                <div className={style.scoretable}>
                  <table className={style.tableleft}>
                    <div className={style.tablemodulescore1}>
                      <div className={style.tableheader}>
                        <tr style={{ width: "100%" }}>
                          <th
                            style={{
                              width: "30%",
                              textAlign: "left",
                              padding: "0 20px",
                            }}
                          >
                            Module Score
                          </th>
                          <th
                            style={{
                              fontWeight: "normal",
                              width: "70%",
                              padding: "0 20px",
                            }}
                          >
                            All score will be applied
                          </th>
                        </tr>
                      </div>
                      <div>
                        <tr style={{ width: "100%" }}>
                          <th style={{ width: "30%", padding: "0 20px" }}>
                            Attendant score
                          </th>
                          <td style={{ width: "70%", paddingLeft: "70px" }}>
                            <Checkbox
                              ref={AttendantRef}
                              defaultChecked={templatedata.attendscore}
                              color="default"
                            />
                          </td>
                        </tr>
                        {/* <tr>
													<th style={{ width: "30%", padding: "0 20px" }}>Quick score</th>
													<td style={{ width: "70%", paddingLeft: "70px" }}>
														<Checkbox
															ref={QuickScoreRef}
															defaultChecked={templatedata.quick}
															color="default"
														/>
													</td>
												</tr> */}
                      </div>
                    </div>
                  </table>
                  <table className={style.tableleft}>
                    <div className={style.tablemodulescore1}>
                      <div className={style.tableheader}>
                        <tr style={{ width: "100%" }}>
                          <th
                            style={{
                              width: "30%",
                              textAlign: "left",
                              paddingLeft: "0",
                            }}
                          >
                            Module Score
                          </th>
                          <th style={{ fontWeight: "normal", width: "70%" }}>
                            All score will be applied
                          </th>
                        </tr>
                      </div>
                      <div>
                        <tr>
                          <th style={{ width: "30%" }}>Audit score</th>
                          <td style={{ width: "70%", paddingLeft: "40px" }}>
                            <Checkbox
                              ref={AuditScoreRef}
                              defaultChecked={templatedata.isaudit}
                              color="default"
                            />
                          </td>
                        </tr>
                        {/* <tr>
													<th style={{ width: "30%" }}>Practice Score</th>
													<td style={{ width: "70%", paddingLeft: "40px" }}>
														<Checkbox
															ref={PracticeScoreRef}
															defaultChecked={templatedata.ispracticescore}
															color="default"
														/>
													</td>
												</tr> */}
                        <tr>
                          <th style={{ width: "30%" }}>GPA</th>
                          <td style={{ width: "70%", paddingLeft: "40px" }}>
                            <Checkbox
                              ref={GPARef}
                              defaultChecked={templatedata.isgpa}
                              color="default"
                            />
                          </td>
                        </tr>
                        <tr>
                          <th style={{ width: "30%" }}>Final Status</th>
                          <td style={{ width: "70%", paddingLeft: "40px" }}>
                            <Checkbox
                              ref={FinalStatusRef}
                              defaultChecked={templatedata.finalstatus}
                              color="default"
                            />
                          </td>
                        </tr>
                      </div>
                    </div>
                  </table>
                </div>

                {/* Button */}
                <div className={style.buttonSubmit}>
                  <button
                    className={style.saveButton}
                    onClick={() => {
                      handleSave();
                    }}
                  >
                    Save
                  </button>

                  <button
                    className={style.cancelButton}
                    onClick={() => navigate("/EmailConfiguration")}
                  >
                    <Link to="#">Cancel</Link>
                  </button>
                </div>
              </div>
            </Form>
          )}
        </div>
      </div>
      <Footer />
    </div>
  );
}
export { DetailPage };
