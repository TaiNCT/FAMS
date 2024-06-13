import React, { useEffect, useState } from "react";
import { Table, Pagination } from "antd";
import { MdOutlineSort } from "react-icons/md";
import { Box } from "@mui/material";
import style from "./style.module.scss";
import { css } from "@emotion/css";
import axios from "../../../../../axiosAuth";

// @ts-ignore
const backend_api: string = `${import.meta.env.VITE_API_HOST}:${
  import.meta.env.VITE_API_PORT
}`;

const modalStyle = css`
  .ant-pagination-options {
    display: none !important;
  }
`;

interface ActivityLogTable {
  no: number;
  dateTime: string;
  modifiedBy: string;
  action: string;
}

interface TablePaginationConfig {
  current?: number;
  pageSize?: number;
}

export interface TableParams {
  pagination?: TablePaginationConfig;
}

const ActivityLogTable: React.FC = () => {
  // const myContext = useContext;
  const [activityData, setActivityData] = useState<ActivityLogTable[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [totalPage, setTotalPage] = useState<number>(0);
  const [paginationConfig, setPaginationConfig] =
    useState<TablePaginationConfig>({
      current: 1,
      pageSize: 5,
    });

  useEffect(() => {
    const fetchActivityData = async () => {
      setIsLoading(true);
      try {
        const response = await axios.get(`${backend_api}/api/Log`);
        if (response.status === 200) {
          const totalItems = response.data.length;
          const pages = Math.ceil(totalItems / paginationConfig.pageSize!);
          setTotalPage(pages);

          const startIndex =
            (paginationConfig.current! - 1) * paginationConfig.pageSize!;
          const endIndex = startIndex + paginationConfig.pageSize!;
          const currentPageData = response.data.slice(startIndex, endIndex);
          setActivityData(currentPageData);
        } else {
          throw new Error("Failed to fetch activity data");
        }
      } catch (error) {
        console.error("Error fetching activity data:", error);
      } finally {
        setIsLoading(false);
      }
    };

    fetchActivityData();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [paginationConfig.current]);

  const columns = [
    {
      title: "No",
      dataIndex: "no",
      key: "no",
      onHeaderCell: () => ({ style: customHeaderStyle }),
      render: (text: number) => <span className={style.boldText}>{text}</span>,
    },
    {
      title: "Datetime",
      dataIndex: "dateTime",
      key: "dateTime",
      sorter: (a, b) => a.dateTime.localeCompare(b.dateTime),
      sortIcon: () => (
        <div>
          <MdOutlineSort />
        </div>
      ),
      onHeaderCell: () => ({ style: customHeaderStyle }),
      render: (text: string) => <span className={style.boldText}>{text}</span>,
    },
    {
      title: "Modified By",
      dataIndex: "modifiedBy",
      key: "modifiedBy",
      onHeaderCell: () => ({ style: customHeaderStyle }),
      render: (text: string) => <span className={style.boldText}>{text}</span>,
    },
    {
      title: "Action",
      dataIndex: "action",
      key: "action",
      onHeaderCell: () => ({ style: customHeaderStyle }),
      render: (text: string) => <span className={style.boldText}>{text}</span>,
    },
  ];

  const customHeaderStyle = {
    background: "#2D3748",
    color: "#fff",
    fontWeight: "bold",
  };

  const handlePaginationChange = (page: number) => {
    setPaginationConfig({
      ...paginationConfig,
      current: page,
    });
  };

  return (
    <div className="email-table-remind">
      <div>
        <div className={style.boxHeader}>
          <Box className={style.customBox}>
            <h5 className={style.activitiesLog}>Activity Log</h5>
          </Box>
        </div>
        <Table
          columns={columns}
          dataSource={activityData}
          pagination={false}
          bordered
          rowKey="no"
          style={{ paddingLeft: "16px", paddingRight: "16px" }}
          loading={isLoading}
        />
      </div>

      <Pagination
        className={modalStyle}
        style={{ marginTop: "10px", textAlign: "center" }}
        current={paginationConfig.current}
        pageSize={paginationConfig.pageSize}
        total={totalPage}
        onChange={handlePaginationChange}
        showSizeChanger={false}
        showLessItems
        showQuickJumper
      />
    </div>
  );
};

export default ActivityLogTable;
