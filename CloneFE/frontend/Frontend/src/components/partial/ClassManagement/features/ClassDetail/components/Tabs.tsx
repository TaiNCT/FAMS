import { ReactElement, useState } from "react";

export type TabType = {
  label: string;
  content: ReactElement;
  onClick?: () => void;
};

type SyllabusTabsPropsType = {
  tabs: TabType[];
};

export function SyllabusTabs({ tabs }: SyllabusTabsPropsType) {
  const [selectedTabIndex, setSelectedTabIndex] = useState<number>(0);

  return (
    <div>
      <div className="flex">
        {tabs.map((tab, index) => {
          return (
            <button
              key={`class-detail-tab-${index}`}
              onClick={() => {
                setSelectedTabIndex(index);
                tab.onClick && tab.onClick(); 
              }}
              className={`text-white w-[200px] py-1 rounded-t-3xl m-0 ${index === selectedTabIndex
                  ? "bg-[#2D3748] hover:bg-[#4c5970]"
                  : "bg-[#6D7684] hover:bg-[#97a0ae]"
                }`}
            >
              {tab.label}
            </button>
          );
        })}
      </div>
      {tabs[selectedTabIndex].content}
    </div>
  );
}