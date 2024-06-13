import React, { useEffect, useState, useRef } from "react";
import axios from "axios";
import { Box } from "@chakra-ui/react";
import Header from "../../components/partial/UserManagement/HeaderComponent/Header";
import Table from "../../components/partial/UserManagement/TableComponent/Table";
import Pagination from "../../components/partial/UserManagement/layouts/Pagination";
import ItemsPerPageDropdown from "../../components/partial/UserManagement/layouts/ItemsPerPageDropdown";
import Navbar from "../../components/layouts/Navbar";
import Sidebar from "../../components/layouts/Sidebar";
import Footer from "../../components/layouts/Footer";
import style from "./style.module.scss";
import useRoleStore from "../../store/roleStore";
import GlobalLoading from "../../components/global/GlobalLoading";

const UserManagement = () => {
  const [isLoading, setIsLoading] = useState(true);
  const [apiData, setApiData] = useState([]);
  const [totalUsers, setTotalUsers] = useState(0);
  const [currentPage, setCurrentPage] = useState(1);
  const [postsPerPage, setPostsPerPage] = useState(10);
  const [filterData, setFilterData] = useState({
    dob: null,
    isAdmin: false,
    isTrainer: false,
    isActive: false,
    isInactive: false,
    isMale: false,
    isFemale: false,
  });
  const roles = useRoleStore((state) => state.roles);
  const setRoles = useRoleStore((state) => state.setRole);
  const [sortBy, setSortBy] = useState("id");
  const [sortOrder, setSortOrder] = useState("desc");
  const [searching, setSearching] = useState(false);
  const fetchDataRef = useRef();
  const [tags, setTags] = useState([]);

  const backend_api = `${import.meta.env.VITE_API_HOST}:${
    import.meta.env.VITE_API_PORT
  }`;

  useEffect(() => {
    const fetchData = async () => {
      setIsLoading(true);
      try {
        let endpoint = "";
        let params = {};
        const config = {
          headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
        };
        if (filterData.keyword && filterData.keyword.trim() !== "") {
          //setCurrentPage(1);
          endpoint = `${backend_api}/api/search-user`;
          params = {
            keyword: filterData.keyword,
            pageNumber: currentPage,
            pageSize: postsPerPage,
            sortField: sortBy,
            sortOrder: sortOrder,
          };
        } else {
          endpoint = `${backend_api}/api/filter-user/${sortBy}`;
          params = {
            page: currentPage,
            pageSize: postsPerPage,
            sortOrder: sortOrder,
            isAdmin: filterData.isAdmin,
            isTrainer: filterData.isTrainer,
            isActive: filterData.isActive,
            isInactive: filterData.isInactive,
            isMale: filterData.isMale,
            isFemale: filterData.isFemale,
          };
          if (filterData.dob !== null) {
            params.dob = filterData.dob;
          }
        }

        const response = await axios.get(endpoint, {
          params: params,
          headers: config.headers,
        });
        const data = response.data;
        setApiData(data.result.users);
        setTotalUsers(data.result.totalCount);
      } catch (error) {
        
      }
      finally {
        setIsLoading(false);
      }
    };

    fetchDataRef.current = fetchData;
    fetchData();

    const config = {
      headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
    };
    const fetchRoles = async () => {
      try {
        const response = await axios.get(`${backend_api}/api/get-role`, config);
        if (response.data && Array.isArray(response.data.result)) {
          setRoles(response.data.result);
        } else {
          console.error(
            "Data received from GetAllRole is not an array",
            response.data
          );
          setRoles([]);
        }
      } catch (error) {
        console.error("Failed to fetch roles", error);
        setRoles([]);
      }
    };
    if (roles.length == 0) fetchRoles();
  }, [
    currentPage,
    postsPerPage,
    sortBy,
    sortOrder,
    filterData.keyword,
    filterData,
  ]);

  const handleSearch = (keyword) => {
    setFilterData({
      ...filterData,
      keyword: keyword,
    });
    setCurrentPage(1);
    setSearching(keyword.trim() !== "");
  };

  const handleFilter = (filterData) => {
    setFilterData({
      ...filterData,
    });
    setCurrentPage(1);
  };

  useEffect(() => {
    const storedTags = localStorage.getItem("tags");
    if (storedTags) {
      setTags(JSON.parse(storedTags));
    }
  }, []);

  const handleItemsPerPageChange = (newItemsPerPage) => {
    setPostsPerPage(newItemsPerPage);
    setCurrentPage(1);
  };

  return (
    <div
      className="flex flex-col min-h-screen overflow-y-hidden"
      style={{ overflowX: "hidden", boxSizing: "border-box" }}
    >
      <GlobalLoading isLoading={isLoading}/>
      <Navbar />
      <div
        className="flex flex-1 overflow-hidden"
        style={{ overflowY: "auto", overflowX: "hidden" }}
      >
        <Sidebar />
        <div className="flex-1 overflow-y-auto" style={{ overflowX: "hidden" }}>
          <Box mb={5}>
            <Header
              tags={tags}
              setTags={setTags}
              onUpdateData={() => fetchDataRef.current()}
              onFilter={handleFilter}
              onSearch={handleSearch}
              filterData={filterData}
            />
            <Table
              userData={apiData}
              sortBy={sortBy}
              sortOrder={sortOrder}
              setSortBy={setSortBy}
              setSortOrder={setSortOrder}
              onUpdateData={() => fetchDataRef.current()}
            />
          </Box>
          <Box
            display="flex"
            justifyContent="space-between"
            alignItems="center"
            px={3}
            mb={5}
          >
            <Box flex={1} display="flex" justifyContent="center">
              <Pagination
                totalPosts={totalUsers}
                postsPerPage={postsPerPage}
                setCurrentPage={setCurrentPage}
                currentPage={currentPage}
              />
            </Box>
            <ItemsPerPageDropdown
              postsPerPage={postsPerPage}
              onItemsPerPageChange={handleItemsPerPageChange}
            />
          </Box>
        </div>
      </div>
      <Footer />
    </div>
  );
};

export { UserManagement };
