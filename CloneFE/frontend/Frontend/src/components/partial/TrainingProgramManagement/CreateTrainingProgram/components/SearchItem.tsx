import style from '../style.module.scss';
import { Syllabus } from '../models/syllabus.model';
import formatDateToYYYYMMDD from '../utils/dateFormat';

interface SearchItemProps {
    item: Syllabus;
    handleOnSearch: (syllabus: Syllabus, searchTerm: string) => void;
}

const SearchItem: React.FC<SearchItemProps> = (props) => {
    
    return (
        <div className={style.searchItem} onClick={() => props.handleOnSearch(props.item, props.item.topicName)}>
            <p>{props.item.topicName}</p>
            <div>
                <span>{props.item.hours}hrs</span>
                <span>{formatDateToYYYYMMDD(props.item.createdDate)} by {props.item.createdBy}</span>
            </div>
        </div>
    );
};

export default SearchItem;