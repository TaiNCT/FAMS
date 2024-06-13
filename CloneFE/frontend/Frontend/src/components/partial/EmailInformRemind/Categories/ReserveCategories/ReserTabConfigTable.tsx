import { useEffect, useState } from "react";
import type { GetProp, TableProps } from "antd";
import { Chip } from "@mui/material";
import { EditOutlined } from "@ant-design/icons";
import { MdOutlineSort } from "react-icons/md";
import { Table, Typography, Pagination } from "antd";
import { Link } from "react-router-dom";
import "../../customize.scss";
import { useContext } from "react";
import { useEmailContext } from "./ReserveCategoryTable";
import { emailContext } from "@/pages/EmailConfigurationPage";
import { context } from "./ReserveCategoryTable";
import GlobalLoading from "../../../../global/GlobalLoading.jsx";
import axios from "../../../../../axiosAuth";

export type TablePaginationConfig = Exclude<
  GetProp<TableProps, "pagination">,
  boolean
>;
export interface TableParams {
  pagination?: TablePaginationConfig;
}
export interface EmailTemplate {
  temId: string;
  name: string;
  status: number;
  description: string;
  type: string;
  applyTo: string;
}

// @ts-ignore
const backend_api: string = `${import.meta.env.VITE_API_HOST}:${
  import.meta.env.VITE_API_PORT
}`;

interface EmailTableProps {
  onDataUpdate: (data: EmailTemplate[]) => void;
  newDataUpdate: EmailTemplate[] | undefined;
  tableParams: TableParams;
  setTableParams: React.Dispatch<React.SetStateAction<TableParams>>;
  searchParams: Record<string, unknown>;
}
export const EmailTable: React.FC<EmailTableProps> = ({
  newDataUpdate,
  tableParams,
  setTableParams,
  searchParams,
}) => {
  const parentContext = useContext(context);
  const myContext = useContext(emailContext);
  const { updatedData } = useEmailContext();
  const [programs, setPrograms] = useState<EmailTemplate[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [isApiRunning, setIsApiRunning] = useState(false); // Thêm biến trạng thái để theo dõi API đang chạy
  const columns: TableProps<EmailTemplate>["columns"] = [
    {
      key: "name",
      title: "Name",
      dataIndex: "name",
      sorter: (a, b) => a.name.localeCompare(b.name),
      render: (programName) => (
        <div>
          <Typography className="ant-table-cell-name">
            {" "}
            {programName}
          </Typography>
        </div>
      ),
      sortIcon: () => (
        <div>
          <MdOutlineSort />
        </div>
      ),
    },
    {
      key: "status",
      title: "Status",
      dataIndex: "status",
      sorter: (a, b) => a.status - b.status,
      render: (status) => (
        <Typography>
          {status === 1 ? (
            <Chip label="Active" sx={{ color: "white", bgcolor: "#2f913f" }} />
          ) : (
            <Chip
              sx={{ color: "white", bgcolor: "#B9B9B9" }}
              label="Inactive"
            />
          )}
        </Typography>
      ),
      sortIcon: () => (
        <div>
          <MdOutlineSort />
        </div>
      ),
    },
    {
      key: "description",
      title: "Description",
      dataIndex: "description",
      sorter: (a, b) => a.description.localeCompare(b.description),
      render: (description) => (
        <div>
          <Typography>{description}</Typography>
        </div>
      ),
      sortIcon: () => (
        <div>
          <MdOutlineSort />
        </div>
      ),
    },
    {
      key: "type",
      title: "Category",
      dataIndex: "type",
      sorter: (a, b) => a.type.localeCompare(b.type),
      render: (category) => (
        <div>
          <Typography>{category}</Typography>
        </div>
      ),
      sortIcon: () => (
        <div>
          <MdOutlineSort />
        </div>
      ),
    },
    {
      key: "applyTo",
      title: "Apply To",
      dataIndex: "applyTo",
      sorter: (a, b) => a.type.localeCompare(b.type),
      render: (applyTo) => (
        <div>
          <Typography>{applyTo}</Typography>
        </div>
      ),
      sortIcon: () => (
        <div>
          <MdOutlineSort />
        </div>
      ),
    },
    {
      key: "action",
      title: "",
      render: (record) => (
        <div>
          <Link to={`/EmailConfiguration/Detail/${record.temId}`}>
            <EditOutlined />
          </Link>
        </div>
      ),
    },
  ];

  useEffect(() => {
    const fetchData = async () => {
      setIsApiRunning(true);
      setIsLoading(true);
      try {
        let url = `${backend_api}/api/emailTemplates/list?page=${
          tableParams.pagination?.current ? tableParams.pagination.current : 0
        }&pageSize=${tableParams.pagination?.pageSize}`;
        let urlTotalPage = `${backend_api}/api/emailTemplates/list?pageSize=${99999}`;

        if (searchParams.status) {
          url += `&status=${searchParams.status}`;
          urlTotalPage += `&status=${searchParams.status}`;
        }

        url += "&type=1";
        urlTotalPage += "&type=1";

        if (searchParams.applyTo) {
          url += `&applyTo=${searchParams.applyTo}`;
          urlTotalPage += `&applyTo=${searchParams.applyTo}`;
        }

        const response = await axios.get(url);

        const totalPageRes = await axios.get(urlTotalPage);
        if (response.status !== 200 || totalPageRes.status !== 200) {
          throw new Error("Failed to fetch data");
        }
        const data = response.data;
        const totalPageData = totalPageRes.data;

        let data_afer_filter;
        if (searchParams.name || searchParams.description) {
          data_afer_filter = data.filter(
            (e) =>
              e.description.includes(searchParams.description) ||
              e.name.includes(searchParams.name)
          ) as EmailTemplate[];
        } else data_afer_filter = data as EmailTemplate[];

        setPrograms(data_afer_filter);
        parentContext.setTotalPage(data_afer_filter.length);

        setIsApiRunning(false);
        setIsLoading(false);
        setTableParams({
          ...tableParams,
          pagination: {
            ...tableParams.pagination,
            total: data.length,
          },
        });
      } catch (error) {
        console.error("Error fetching data:", error);
      }
    };
    fetchData();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [searchParams, myContext.refresh]);

  useEffect(() => {
    if (newDataUpdate) {
      setPrograms(newDataUpdate);
    }
  }, [newDataUpdate]);

  const handleTableChange: TableProps["onChange"] = (pagination) => {
    setTableParams((prevParams) => ({
      ...prevParams,
      pagination: {
        ...prevParams.pagination,
        current: pagination.current,
      },
    }));
    if (pagination.pageSize !== tableParams.pagination?.pageSize) {
      setPrograms([]);
    }
  };

  const tableContainerStyle = {
    marginTop: "20px",
    width: "100%",
  };

  const customHeaderStyle = {
    background: "#2D3748",
    color: "#fff",
  };
  return (
    <div className="email-table-remind">
      <GlobalLoading isLoading={isLoading} />
      <div style={tableContainerStyle}>
        <Table
          sortDirections={["ascend", "descend"]}
          columns={columns.map((col) => ({
            ...col,
            onHeaderCell: () => ({ style: customHeaderStyle }),
          }))}
          dataSource={updatedData == null ? programs : updatedData}
          // loading={isLoading}
          pagination={tableParams.pagination}
          onChange={handleTableChange}
          className="no-pagination-table"
        />
      </div>
      <Pagination
        style={{ marginTop: "10px", textAlign: "center" }}
        current={tableParams.pagination?.current}
        pageSize={tableParams.pagination?.pageSize}
        total={parentContext.totalPage}
        showSizeChanger
        showLessItems
        showQuickJumper
        pageSizeOptions={["5", "10", "20", "30"]}
        disabled={isApiRunning}
        onChange={(page, pageSize) => {
          if (!isApiRunning) {
            // Only allow changing page when API is not running
            setTableParams((prevParams) => ({
              ...prevParams,
              pagination: {
                ...prevParams.pagination,
                current: page,
                pageSize: pageSize,
              },
            }));
          }
        }}
      />
    </div>
  );
};
