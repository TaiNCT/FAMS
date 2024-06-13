import React, { ReactElement, ReactNode, useState } from "react";
import ArrowDropDownCircleIcon from "@/assets/icons/action-icons/ArrowDropDownCircleIcon";

type CollapseProps = {
  children: ReactElement | ReactElement[];
  title: string;
  icon: ReactNode;
  description?: string | ReactElement;
};
const Collapse = ({
  children,
  title,
  icon,
  description,
  ...props
}: CollapseProps & React.HTMLAttributes<HTMLDivElement>) => {
  const [isCollapse, setIsCollapse] = useState(false);
  const handleToggleCollapseClick = () => {
    setIsCollapse(!isCollapse);
  };
  return (
    <div {...props}>
      <div className="flex justify-between bg-[#0B2136] text-white py-2 px-5 rounded-xl gap-3 shadow-lg">
        <div className="flex gap-3 items-center flex-1">
          {icon} <h3 className="font-bold">{title}</h3>
          {description && <div className="flex-1">{description}</div>}
        </div>
        <button onClick={handleToggleCollapseClick}>
          <ArrowDropDownCircleIcon />
        </button>
      </div>
      {!isCollapse && <div className="shadow-lg rounded-lg">{children}</div>}
    </div>
  );
};

export default Collapse;
