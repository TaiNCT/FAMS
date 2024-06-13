import { Box, Flex, Grid } from "@chakra-ui/react";
import React, { useState } from "react";
import moment from "moment";
import ActionMenu from "../../components/ActionMenu/ActionMenu";
import TableHeader from "./TableHeader";
import styles from "./Table.module.scss";
import { TrainingProgram } from "../models/TrainingProgram.model";
import { useNavigate } from "react-router-dom";
import { useTrainingProgramDataContext } from "../../contexts/DataContext";

interface Props {
  data: TrainingProgram[];
  rotation: number;
  setRotation: React.Dispatch<React.SetStateAction<number>>;
  handleSort: (field: string) => void;
  setSelectedColumn: React.Dispatch<React.SetStateAction<string>>;
  selectedColumn: string;
  setOldSelectedColumn: React.Dispatch<React.SetStateAction<string>>;
  oldSelectedColumn: string;
}

const Table: React.FC<Props> = ({
  data,
  rotation,
  setRotation,
  handleSort,
  setSelectedColumn,
  selectedColumn,
  setOldSelectedColumn,
  oldSelectedColumn,
}) => {
  const headerData = [
    { text: "ID", field: "id", size: 2 },
    { text: "Program name", field: "programname", size: 2 },
    { text: "Created on", field: "createdOn", size: 2 },
    { text: "Created by", field: "createdBy", size: 2 },
    { text: "Duration", field: "duration", size: 1 },
    { text: "Status", field: "status", size: 2 },
    { text: "", field: "", size: 2 },
  ];
  const { currentPage, rowsPerPage } = useTrainingProgramDataContext();
  const [hoveredRow, setHoveredRow] = useState<number | null>(null);
  const navigate = useNavigate();

  const handleClick = (text: string, field: string) => {
    setSelectedColumn(text);
    setOldSelectedColumn(selectedColumn);
    handleSort(field);
  };

  const renderTableCell = (
    id: number,
    trainingProgramCode: string,
    value: string | number | React.ReactNode,
    px: number,
    py: number,
    pl: number,
    pr: number,
    style?: string
  ) => {
    const cellStyle =
      hoveredRow === id
        ? `${styles.table_cell_hover} ${styles.table_cell}`
        : `${styles.table_cell}`;

    return (
      <Flex
        px={px}
        py={py}
        pl={pl}
        pr={pr}
        className={`${cellStyle} ${style}`}
        onMouseEnter={() => setHoveredRow(id)}
        onMouseLeave={() => setHoveredRow(null)}
        onClick={() => {
          navigate(`/trainingprogram/${trainingProgramCode}`);
        }}
      >
        {value}
      </Flex>
    );
  };

  return (
    <Box className={styles.table_container}>
      <Grid className={styles.table_grid}>
        {headerData.map((header, index) => (
          <TableHeader
            key={index}
            field={header.field}
            text={header.text}
            size={header.size}
            handleClick={handleClick}
            selectedColumn={selectedColumn}
            oldSelectedColumn={oldSelectedColumn}
            rotation={rotation}
            setRotation={setRotation}
          />
        ))}
        {data.map((program, index) => (
          <React.Fragment key={`program-${program.id}`}>
            {renderTableCell(
              program.id,
              program.trainingProgramCode,
              (currentPage - 1) * rowsPerPage + index + 1,
              0,
              3,
              6,
              0,
              styles.table_cell_id
            )}
            {renderTableCell(
              program.id,
              program.trainingProgramCode,
              program.name,
              1,
              3,
              10,
              20,
              styles.table_cell_program_name
            )}
            {renderTableCell(
              program.id,
              program.trainingProgramCode,
              moment(program.createdDate).format("DD/MM/YYYY"),
              10,
              3,
              10,
              1
            )}
            {renderTableCell(
              program.id,
              program.trainingProgramCode,
              program.createdBy,
              10,
              3,
              10,
              1
            )}
            {renderTableCell(
              program.id,
              program.trainingProgramCode,
              `${program.days} days`,
              10,
              3,
              10,
              1
            )}
            {renderTableCell(
              program.id,
              program.trainingProgramCode,
              <Box
                as="span"
                bgColor={
                  program.status === "Draft"
                    ? "#285D9A"
                    : program.status === "Active"
                    ? "#2f913f"
                    : "#2D3748"
                }
                py={1}
                px={3}
                borderRadius="15px"
                className={styles.table_cell_status}
              >
                {program.status}
              </Box>,
              10,
              3,
              8,
              1
            )}
            <Flex
              py={3}
              px={5}
              className={`${styles.table_cell_menu_btn} ${
                hoveredRow === program.id && styles.table_cell_hover
              }`}
              onMouseEnter={() => setHoveredRow(program.id)}
              onMouseLeave={() => setHoveredRow(null)}
            >
              <ActionMenu
                isDetail={false}
                trainingProgramCode={program.trainingProgramCode}
                id={program.id}
                status={program.status}
                trainingProgramName={program.name}
              />
            </Flex>
          </React.Fragment>
        ))}
      </Grid>
    </Box>
  );
};

export default Table;
