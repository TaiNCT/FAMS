import SearchItem from "./SearchItem";
import style from "../style.module.scss";
import { Syllabus } from "../models/syllabus.model";

interface SearchListProps {
  items: Syllabus[];
  searchValue: string;
  handleOnSearch: (syllabus: Syllabus, searchTerm: string) => void;
}

const SearchList: React.FC<SearchListProps> = (props) => {
  return (
    <>
      {props.searchValue && (
        <div className={style.searchList}>
          {props.items
            .filter((item) => {
              const searchTerm = props.searchValue.toLowerCase();
              const name = item.topicName.toLowerCase();
                
              return searchTerm && name.includes(searchTerm);
            })
            .map((item) => (
              <SearchItem
                item={item}
                handleOnSearch={props.handleOnSearch}
                key={item.id}
              />
            ))}
        </div>
      )}
    </>
  );
};

export default SearchList;
