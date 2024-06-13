import { useEffect } from "react";
import Navbar from "../../components/layouts/Navbar";
import Sidebar from "../../components/layouts/Sidebar";
import Footer from "../../components/layouts/Footer";
import { useAppSelector } from "../../hooks/useRedux";
import { useDispatch } from "react-redux";
import {
  addClassUser,
  deleteClassUser,
  getAllAttendeeType,
  getClassById,
  updateClass,
  updateClassAttendeeType,
  updateClassFsu,
  updateClassTrainingProgram,
} from "../../lib/api/ClassApi";
import { useNavigate, useParams } from "react-router-dom";
import {
  set,
  setTrainingProgram,
  setSyllabusList,
  setAttendeeTypes,
} from "../../lib/redux/classSlice";
import Header from "@/components/partial/ClassManagement/features/EditClass/Header";
import Attendee from "@/components/partial/ClassManagement/features/ClassDetail/components/Attendee";
import { SyllabusTabs } from "@/components/partial/ClassManagement/features/ClassDetail/components/Tabs";
import AttendeeList from "@/components/partial/ClassManagement/features/ClassDetail/components/Tabs/AttendeeList";
import Budget from "@/components/partial/ClassManagement/features/ClassDetail/components/Tabs/Budget";
import Others from "@/components/partial/ClassManagement/features/ClassDetail/components/Tabs/Others";
import EditTrainingProgram from "@/components/partial/ClassManagement/features/EditClass/EditTrainingProgram";
import EditTimeFrame from "@/components/partial/ClassManagement/features/EditClass/EditTimeFrame";
import { setAdmins } from "@/lib/redux/userSlice";
import { getAllAdmin } from "@/lib/api/UserApi";
import { setFsus } from "@/lib/redux/fsuSlice";
import { Form, FormProps } from "antd";
import EditGeneral from "@/components/partial/ClassManagement/features/EditClass/EditGeneral";
import { getAllFsus } from "@/lib/api/FsuApi";
import EditAttendee from "@/components/partial/ClassManagement/features/EditClass/EditAttendee";
import { RootState } from "@/lib/redux/store";
import { getAllTrainingProgram } from "@/lib/api/TrainingProgramApi";
import { setTrainingPrograms } from "@/lib/redux/trainingProgramSlice";
import { toast } from "react-toastify";
const EditClassPage = () => {
  const data = useAppSelector((state) => state.class.data);
  const fsus = useAppSelector((state: RootState) => state.fsu.fsus);
  const trainingProgram = useAppSelector(
    (state) => state.class.trainingProgram
  );
  const trainingPrograms = useAppSelector(
    (state) => state.trainingPrograms.trainingPrograms
  );
  const navigate = useNavigate();
  const [form] = Form.useForm();

  const { id } = useParams<string>();
  const dispatch = useDispatch();
  const attendeeTypes = useAppSelector((state) => state.class.attendeeTypes);

  type FieldType = {
    durationTime: any[];
    durationDate: any[];
    adminId: string;
    fsu: any;
    attendeeTypeName: string;
    plannedAttendee: number;
    acceptedAttendee: number;
    actualAttendee: number;
    trainingProgramId: string;
  };
  const onFinish: FormProps<FieldType>["onFinish"] = (values) => {
    const fsu = fsus.find((item) => item.fsuId == values.fsu);
    const attendeeType = attendeeTypes.find(
      (item) => item.attendeeTypeName == values.attendeeTypeName
    );
    form.setFieldsValue({
      trainingProgramId: trainingProgram.trainingProgramCode,
    });
    const oldAdminId = data.users.find(
      (user) => user.roleName == "Admin"
    )?.userId;
    if (oldAdminId) {
      deleteClassUser(oldAdminId, data.classId);
    }
    if (attendeeType) {
      updateClassAttendeeType(data.classId, {
        attendeeTypeId: attendeeType.attendeeTypeId,
        attendeeTypeName: attendeeType.attendeeTypeName,
      });
    }
    if (trainingProgram) {
      updateClassTrainingProgram(data.classId, {
        trainingProgramCode: trainingProgram.trainingProgramCode,
        updatedBy: trainingProgram.updatedBy,
        updatedDate: trainingProgram.updatedDate,
        days: trainingProgram.days,
        hours: trainingProgram.hours,
        name: trainingProgram.name,
      });
    }
    setTimeout(() => {
      addClassUser(values.adminId, data.classId);
    }, 500);
    updateClassFsu(data.classId, fsu);
    const request = {
      classId: data.classId,
      startTime:
        values.durationTime[0].$H.toString().padStart(2, "0") +
        ":" +
        values.durationTime[0].$m.toString().padStart(2, "0") +
        ":00",
      endTime:
        values.durationTime[1].$H.toString().padStart(2, "0") +
        ":" +
        values.durationTime[1].$m.toString().padStart(2, "0") +
        ":00",
      startDate: new Date(values.durationDate[0].$d).toISOString().slice(0, 10),
      endDate: new Date(values.durationDate[1].$d).toISOString().slice(0, 10),
      acceptedAttendee: values.acceptedAttendee,
      actualAttendee: values.actualAttendee,
      plannedAttendee: values.plannedAttendee,
      // attendeeTypeName: values.attendeeTypeName,
      // fsu: {
      //   fsuId: fsu.fsuId,
      //   name: fsu.name,
      //   email: fsu.email,
      // },
      // trainingProgram: {
      //   trainingProgramCode: trainingProgram.trainingProgramCode,
      //   updatedBy: trainingProgram.updatedBy,
      //   updatedDate: trainingProgram.updatedDate,
      //   days: trainingProgram.days,
      //   hours: trainingProgram.hours,
      //   name: trainingProgram.name,
      // },
    };

    handleUpdate(request);
    toast.success("Update class successfully");
  };

  const handleUpdate = async (request) => {

    const { data, error } = await updateClass(request);
    setTimeout(() => {
      navigate(`/class-detail/${id}`);
    }, 1000);
  };

  const onFinishFailed: FormProps<FieldType>["onFinishFailed"] = (
    errorInfo
  ) => {
  };

  useEffect(() => {
    const fetchData = async () => {
      if (id) {
        getClassById(id)
          .then((res) => {
            dispatch(set(res.data.data));
            dispatch(setTrainingProgram(res.data.data.trainingProgram));
            dispatch(setSyllabusList(res.data.data.syllabus));
          })
          .catch((error) => {
          });
        getAllAdmin().then((res) => {
          dispatch(setAdmins(res.data.data.userBasicDto));
        });
        getAllFsus().then((res) => {
          dispatch(setFsus(res.data.items));
        });
        getAllAttendeeType().then((res) => {
          dispatch(setAttendeeTypes(res.data.attendeeTypes));
        });
        getAllTrainingProgram().then((res) => {
          dispatch(setTrainingPrograms(res.data.trainingList));
        });
      }
    };
    fetchData();
  }, []);
  if (!data) return;
  return (
    <div className="min-h-[100vh] flex flex-col">
      <Navbar />
      <div className="flex-1 flex">
        <Sidebar />
        <div className="flex-1">
          <Header />
          <Form
            form={form}
            name="basic"
            initialValues={{ remember: true }}
            onFinish={onFinish}
            onFinishFailed={onFinishFailed}
            autoComplete="off"
          >
            <div className="p-3">
              <div className="grid-cols-3 grid gap-3 mb-5 ">
                <div className="col-span-1 flex flex-col gap-2">
                  <EditGeneral />
                  <EditAttendee />
                </div>
                <div className="col-span-2">
                  <EditTimeFrame />
                </div>
              </div>
              <SyllabusTabs
                tabs={[
                  {
                    label: "Training Program",
                    content: <EditTrainingProgram classId={id} form={form} />,
                  },
                  {
                    label: "Attendee List",
                    content: <AttendeeList />,
                  },
                  {
                    label: "Budget",
                    content: <Budget />,
                  },
                  {
                    label: "Others",
                    content: <Others />,
                  },
                ]}
              />
            </div>
          </Form>
        </div>
      </div>
      <Footer />
    </div>
  );
};

export default EditClassPage;
