import TablePagination from '@mui/material/TablePagination';
import { useState } from 'react';

export default function RowsPerPage({ setRowsPerPage }) {
  const [page, setPage] = useState(1);
  const [rowsPerPage, setRowsPerPageLocal] = useState(25);

  const handleChangePage = (
    event: React.MouseEvent<HTMLButtonElement> | null,
    newPage: number,
  ) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
  ) => {
    const newRowsPerPage = parseInt(event.target.value, 10);
    setRowsPerPageLocal(newRowsPerPage);
    setRowsPerPage(newRowsPerPage);
    setPage(0);
  };

  return (
    <TablePagination
      component="div"
      rowsPerPageOptions={[2, 3, 5, 10, 25]}
      count={100}
      page={page}
      onPageChange={handleChangePage}
      rowsPerPage={rowsPerPage}
      onRowsPerPageChange={handleChangeRowsPerPage}
    />
  );
}
