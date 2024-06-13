import CopyIcon from "../../../../../assets/icons/document-manage-icons/CopyIcon";
import CreateIcon from "../../../../../assets/icons/document-manage-icons/CreateIcon";
import DeleteForeverIcon from "../../../../../assets/icons/document-manage-icons/DeleteForeverIcon";
import DownloadIcon from "../../../../../assets/icons/document-manage-icons/DownloadIcon";
import PublishIcon from "../../../../../assets/icons/document-manage-icons/PublishIcon";
import SearchIcon from "../../../../../assets/icons/document-manage-icons/SearchIcon";
import UploadIcon from "../../../../../assets/icons/document-manage-icons/UploadIcon";
type DocumentManageIconsPropsType = {
  icon:
  | "copy"
  | "create"
  | "delete-forever"
  | "download"
  | "publish"
  | "search"
  | "upload";
};
const DocumentManageIcons = ({
  icon,
  ...props
}: DocumentManageIconsPropsType & React.HTMLAttributes<SVGSVGElement>) => {
  switch (icon) {
    case "copy": {
      return <CopyIcon {...props} />;
    }
    case "create": {
      return <CreateIcon {...props} />;
    }
    case "delete-forever": {
      return <DeleteForeverIcon {...props} />;
    }
    case "download": {
      return <DownloadIcon {...props} />;
    }
    case "publish": {
      return <PublishIcon {...props} />;
    }
    case "search": {
      return <SearchIcon {...props} />;
    }
    case "upload": {
      return <UploadIcon {...props} />;
    }
  }
};

export default DocumentManageIcons;
