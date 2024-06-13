import Pagination from '@mui/material/Pagination';
import Stack from '@mui/material/Stack';
import { motion } from 'framer-motion'
import { Box } from '@mui/material';
import { useEffect, useState } from 'react';
import { SyllabusList } from '../types';
import ApiClient from '../ApiClient';

const apiClient = new ApiClient();
export let listSyllabusService: Array<SyllabusList> = [];
let totalRows: number = 0;

function PaginationButtons({ rows, setRows, rowsPerPage, startDate, endDate, tags, isLoading, setIsLoading, searchValue }: { searchValue: string; rows?: any, setRows?: any, rowsPerPage?: any, startDate?: any, endDate?: any, tags: Array<string>, isLoading?: boolean, setIsLoading?: (isLoading: boolean) => void }) {
  const paginationVariants = {
    hidden: {
      opacity: 0,
      y: 100,
    },
    visible: {
      opacity: 1,
      y: 0,
      transition: {
        type: "spring",
        stiffness: 260,
        damping: 20,
        duration: 0.5,
      },
    },
  }

  const [pagination, setPagination] = useState({
    pageNumber: 1,
    pageSize: rowsPerPage,
  })

  async function fetchData(pageNumber: number, pageSize: number) {
    try {
      if (pagination) {
        const data = await apiClient.getSyallabusList(pageNumber, rowsPerPage);
        if (data.isSuccess) {
          setRows(data.result.data);
          totalRows = data.result.metadata.totalItems;
        }
      }
    } catch (error) {
      console.error('Failed to fetch data:', error);
    }
  }

  async function fetchSearchDataWithKeyWords() {
    try {
      const data = await apiClient.searchSyllabusByKeyword(searchValue.trim(), pagination.pageNumber, rowsPerPage);
      if (data.isSuccess) {
        setRows(data.result.data);
        totalRows = data.result.metadata.totalItems;
      }
    } catch (error) {
      console.error('Failed to fetch data:', error);
    }
  }

  async function fetchDataWithDate(startDate: Date, endDate: Date) {
    try {
      const data = await apiClient.searchSyllabusByDate(startDate.toISOString(), endDate.toISOString(), pagination.pageNumber, rowsPerPage);
      if (data.isSuccess) {
        setRows(data.result.data);
        totalRows = data.result.metadata.totalItems;
      }
    } catch (error) {
      console.error('Failed to fetch data:', error);
    }
  }

  const handlePageChange = (_event: any, page: number) => {
    setPagination(prevPagination => ({ ...prevPagination, pageNumber: page }));
  }

  useEffect(() => {
    fetchData(pagination.pageNumber, rowsPerPage)
  }, []);

  useEffect(() => {
    if (startDate && endDate) fetchDataWithDate(startDate, endDate);
    else if (searchValue.trim().length > 0) fetchSearchDataWithKeyWords();
    else fetchData(pagination.pageNumber, rowsPerPage);
  }, [pagination]); // Fetch data whenever the pagination state changes

  useEffect(() => {
    if (pagination.pageNumber > 1) pagination.pageNumber = 1;
    if (searchValue.trim().length > 0) {
      fetchSearchDataWithKeyWords();
    }
    else if (startDate && endDate) {
      fetchDataWithDate(startDate, endDate)
    }
    else {
      fetchData(pagination.pageNumber, rowsPerPage)
    }
  }, [searchValue, startDate, endDate]);

  useEffect(() => {
    if (pagination.pageNumber > 1) pagination.pageNumber = 1;
    if (startDate && endDate) fetchDataWithDate(startDate, endDate);
    else if (searchValue.trim().length > 0) fetchSearchDataWithKeyWords();
    else fetchData(pagination.pageNumber, rowsPerPage);
  }, [rowsPerPage])

  useEffect(() => {
    if (tags.length > 0) fetchSearchDataWithKeyWords();
    else fetchData(pagination.pageNumber, rowsPerPage)
  }, [tags]);

  useEffect(() => {
    if (isLoading === true) {
      fetchData(pagination.pageNumber, rowsPerPage)
      setIsLoading(false);
    }
  }, [isLoading]);

  return (
    <motion.div variants={paginationVariants}
      animate="visible" >
      <div >
        <Box justifyContent={"center"} alignItems={"justify"} display={"flex"}
          sx={{
            margin: "30px 0px"
          }}>

          <Stack justifyContent={"center"} alignItems={"justify"} spacing={2} color={"blue"}>

            <Pagination
              count={Math.ceil(totalRows / rowsPerPage)}
              onChange={handlePageChange}
              page={pagination.pageNumber}
              color='primary'
              showLastButton
              sx={{
                "& .MuiPagination-ul": { backgroundColor: "white", },
                "& .MuiPaginationItem-page": { backgroundColor: "#E2E8F0", color: "black", border: "1px hidden ", fontWeight: '700' },
                "& .Mui-selected": { color: "white", backgroundColor: "black" },
                '& .MuiPaginationItem-root.Mui-selected': { backgroundColor: '#2D3748' },
                '& .MuiPaginationItem-root.Mui-selected:hover': { backgroundColor: '#555' }
              }}
            />
          </Stack>
        </Box>
      </div>
    </motion.div>
  )
}


export default PaginationButtons
