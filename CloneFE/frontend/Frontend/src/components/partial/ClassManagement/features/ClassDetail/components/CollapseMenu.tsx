import { ReactNode, useEffect, useRef, useState } from "react";
import ArrowBackIosIcon from "@/assets/icons/navigator-icons/ArrowBackIosIcon";
import { Link } from "react-router-dom";

export type CollapseItemType = {
  title: string;
  href: string;
};

type CollapseProps = {
  icon: ReactNode;
  title: string;
  items: CollapseItemType[];
  isOpen: boolean;
};
const CollapseMenu = ({ icon, title, items, isOpen }: CollapseProps) => {
  const [height, setHeight] = useState(0);
  const ref = useRef<HTMLDivElement>(null);
  const [isCollapsed, setIsCollapsed] = useState(true);
  const handleToggleCollapse = () => {
    setIsCollapsed(!isCollapsed);
  };
  useEffect(() => {
    if (ref.current) {
      setHeight(ref.current.clientHeight);
    }
  }, []);

  return (
    <div
      className={`duration-100 overflow-hidden ${
        isOpen ? "w-[256px]" : "w-[22px]"
      }`}
    >
      <button
        className="flex justify-between w-full"
        onClick={handleToggleCollapse}
      >
        <div className="flex gap-4">
          {icon} {isOpen && title}
        </div>
        {isOpen && (
          <div>
            <ArrowBackIosIcon
              className={`${isCollapsed && "-rotate-90"} duration-100`}
            />
          </div>
        )}
      </button>
      <div
        className={`overflow-hidden duration-100 ${
          isCollapsed || !isOpen ? "h-0" : "h-[" + height + "px]"
        }`}
      >
        <div className={`flex flex-col gap-2 pt-2`} ref={ref}>
          {items.map((item, index) => (
            <Link
              to={item.href}
              className="text-center p-1 bg-[#ECF8FF]"
              key={`side-menu-${title}-sub-item-${index}`}
            >
              {item.title}
            </Link>
          ))}
        </div>
      </div>
    </div>
  );
};

export default CollapseMenu;
