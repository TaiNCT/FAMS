import React, {
  useState,
  useCallback,
  useEffect,
  forwardRef,
  useImperativeHandle,
} from "react";
import {
  Popover,
  PopoverTrigger,
  PopoverContent,
  PopoverBody,
  PopoverFooter,
  Flex,
  Text,
  Input,
  Select,
  Button,
  ButtonGroup,
} from "@chakra-ui/react";
import { IoFilter } from "react-icons/io5";
import styles from "./Filter.module.scss";
import { getAuthors } from "../services/TrainingProgramService";

interface FilterProps {
  onFilter: (values: Record<string, string | string[]>) => void;
  handleResetTable: (filter?: boolean) => void;
}

const Filter = forwardRef<any, FilterProps>(
  ({ onFilter, handleResetTable }, ref) => {
    const [programTimeFrameFrom, setProgramTimeFrameFrom] = useState("");
    const [programTimeFrameTo, setProgramTimeFrameTo] = useState("");
    const [status, setStatus] = useState<string[]>([]);
    const [createdBy, setCreatedBy] = useState("");
    const [authors, setAuthors] = useState<string[]>([]);
    const statuses = ["Active", "Inactive", "Draft"];

    const handleSearch = () => {
      onFilter({
        programTimeFrameFrom,
        programTimeFrameTo,
        status,
        createdBy,
      });
    };

    const handleClearFilter = () => {
      setProgramTimeFrameFrom("");
      setProgramTimeFrameTo("");
      setStatus([]);
      setCreatedBy("");
      handleResetTable(true);
    };

    useImperativeHandle(ref, () => ({
      handleClearFilter
    }));

    const handleStatusChange = (e: React.ChangeEvent<HTMLInputElement>) => {
      const { value, checked } = e.target;
      setStatus((prevStatus) => {
        if (checked) {
          return [...prevStatus, value];
        } else {
          return prevStatus.filter((status) => status !== value);
        }
      });
    };   

    useEffect(() => {
      const fetchAuthors = async () => {
        try {
          const result = await getAuthors();
          if (result != null) setAuthors(result);
        } catch (e) {
          console.error(e);
        }
      };
    
      fetchAuthors();
    }, []);

    return (
      <Popover>
        <PopoverTrigger>
          <Button className={styles.filter_button}>
            <Text as="span" fontSize="25px" me={3}>
              <IoFilter />
            </Text>
            Filter
          </Button>
        </PopoverTrigger>
        <PopoverContent className={styles.popover_content}>
          <PopoverBody className={styles.popover_body_container}>
            <Flex alignItems="flex-start" flexDirection="column" mb={6}>
              <Text className={styles.text}>Program Time Frame</Text>
              <Flex alignItems="center" flexDirection="row">
                <label className={styles.sub_title}>from</label>
                <Input
                  onChange={(e) => setProgramTimeFrameFrom(e.target.value)}
                  value={programTimeFrameFrom}
                  placeholder="Select Date and Time"
                  size="md"
                  type="date"
                />
                <label className={`${styles.ml_11} ${styles.sub_title}`}>
                  to
                </label>
                <Input
                  onChange={(e) => setProgramTimeFrameTo(e.target.value)}
                  value={programTimeFrameTo}
                  placeholder="Select Date and Time"
                  size="md"
                  type="date"
                />
              </Flex>
            </Flex>
            <Flex alignItems="flex-start" mb={6}>
              <Text className={styles.text}>Status</Text>
              <Flex flexDirection="column" className={styles.ml_07}>
                {statuses.map((statusArr, i) => (
                  <label key={i} htmlFor={statusArr}>
                    <input
                      id={statusArr}
                      type="checkbox"
                      onChange={handleStatusChange}
                      value={statusArr}
                      checked={status.includes(statusArr)}
                      className={styles.mr_07}
                    />
                    {statusArr}
                  </label>
                ))}
              </Flex>
            </Flex>
            <Flex alignItems="flex-start" mb={6}>
              <Text className={styles.text}>Create by</Text>
              <Flex flexDirection="column" className={styles.ml_07}>
                <Select
                  onChange={(e) => setCreatedBy(e.target.value)}
                  value={createdBy}
                  placeholder="Select name"
                  w="365px"
                >
                  {authors.map((author, i) => (
                    <option key={i} value={author}>
                      {author}
                    </option>
                  ))}
                </Select>
              </Flex>
            </Flex>
          </PopoverBody>
          <PopoverFooter
            border="0"
            display="flex"
            alignItems="flex-end"
            justifyContent="right"
            pb={4}
          >
            <ButtonGroup size="md">
              <Button
                className={styles.sub_filter_button}
                onClick={handleClearFilter}
              >
                Clear
              </Button>
              <Button
                className={styles.sub_filter_button}
                onClick={handleSearch}
              >
                Search
              </Button>
            </ButtonGroup>
          </PopoverFooter>
        </PopoverContent>
      </Popover>
    );
  }
);

export default Filter;
