import TableCell from "@mui/material/TableCell";
import TableRow from "@mui/material/TableRow";
import InventoryIcon from "@mui/icons-material/Inventory";
import { Empty } from 'antd';
function NoDataPage() {
  return (
    <>
      <TableRow
        style={{ textAlign: "center" }}
        sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
      >
        <TableCell
          style={{ fontWeight: "600" }}
          component="th"
          scope="row"
          colSpan={8}
        >
          <Empty image={Empty.PRESENTED_IMAGE_SIMPLE} />
        </TableCell>
      </TableRow>
    </>
  );
}

export default NoDataPage;
