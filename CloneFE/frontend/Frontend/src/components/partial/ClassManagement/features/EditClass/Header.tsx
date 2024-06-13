import MoreHorizontalIcon from "@/assets/icons/action-icons/MoreHorizontalIcon";
import { Flex, Popover } from "antd";
import CreateIcon from "@/assets/icons/document-manage-icons/CreateIcon";
import { Link } from "react-router-dom";
import DeliveryType from "@/components/global/DeliveryType";
import { useAppSelector } from "@/hooks/useRedux";

const Header = () => {
  const data = useAppSelector((state) => state.class.data);

  if (!data) return;
  return (
    <div className="text-white bg-[#2D3748] py-4 px-7">
      <div className="text-lg">Class</div>
      <div className="flex">
        <div>
          <div className="font-semibold text-4xl pb-2">{data.className}</div>
          <div className="font-semibold border-b border-b-white pb-1">
            {data.classCode}
          </div>
        </div>
        <div className="flex-1 flex">
          <div className="p-3">
            <div className="bg-[#B9B9B9] rounded-full px-2 border-2 border-white">
              {data.classStatus}
            </div>
          </div>
        </div>
        <div className="text-end p-3">
        
        </div>
      </div>
      <div className="flex items-center pt-1 gap-3">
        <div>
          <span className="text-2xl font-bold">{data.totalDays}</span>
          &nbsp;days&nbsp;
          <span className="italic">({data.totalHours} hours)</span>
        </div>
        |
        <DeliveryType icon="lab" />
        <DeliveryType icon="lecture" />
        <DeliveryType icon="exam" />
        <DeliveryType icon="workshop" />
        <DeliveryType icon="review" />
      </div>
    </div>
  );
};

export default Header;
