import GradeIcon from "@/assets/icons/indicator-icons/GradeIcon";
import Collapse from "@/components/global/Collapse";
import { useAppSelector } from "@/hooks/useRedux";
import { Form, Input, Select } from "antd";

const EditAttendee = ({ ...props }: React.HTMLAttributes<HTMLDivElement>) => {
  const { Option } = Select;
  const data = useAppSelector((state) => state.class.data);
  const attendeeTypes = useAppSelector((state) => state.class.attendeeTypes);
  if (!data || !attendeeTypes) return;

  return (
    <Collapse
      icon={<GradeIcon />}
      title="Attendee"
      description={
        <Form.Item
          name="attendeeTypeName"
          noStyle
          initialValue={data.attendeeTypeName}
        >
          <Select
            placeholder="Select attendee type"
            allowClear
            style={{ width: 200 }}
          >
            {attendeeTypes?.map((attendeeType: any, index: number) => {
              return (
                <Option
                  value={attendeeType.attendeeTypeName}
                  key={`attendee-type-${index}`}
                >
                  {attendeeType.attendeeTypeName}
                </Option>
              );
            })}
          </Select>
        </Form.Item>
      }
      {...props}
    >
      <div className="grid grid-cols-3 rounded-xl overflow-hidden">
        <div className="bg-[#2D3748] p-5 border border-white flex flex-col items-center justify-between text-white gap-3">
          <div className="font-bold">Planned</div>
          <Form.Item
            name="plannedAttendee"
            noStyle
            initialValue={data.plannedAttendee}
          >
            <Input type="number" />
          </Form.Item>
        </div>
        <div className="bg-blue-800 p-5 border border-white flex flex-col items-center justify-between text-white gap-3">
          <div className="font-bold">Accepted</div>
          <Form.Item
            name="acceptedAttendee"
            noStyle
            initialValue={data.acceptedAttendee}
          >
            <Input type="number" />
          </Form.Item>
        </div>
        <div className="bg-secondary p-5 border-white flex flex-col items-center justify-between gap-3">
          <div className="font-bold">Actual</div>
          <Form.Item
            name="actualAttendee"
            noStyle
            initialValue={data.actualAttendee}
          >
            <Input type="number" />
          </Form.Item>
        </div>
      </div>
    </Collapse>
  );
};

export default EditAttendee;
