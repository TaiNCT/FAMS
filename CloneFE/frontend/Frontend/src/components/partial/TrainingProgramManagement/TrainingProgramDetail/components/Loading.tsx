import { CircularProgress } from "@mui/material";

interface LoadingProps {
  isLoading: boolean;
}

const Loading: React.FC<LoadingProps> = ({ isLoading }) => {
  return (
    <div
      style={{
        position: "fixed",
        top: 0,
        left: 0,
        width: "100%",
        height: "100%",
        backgroundColor: "rgba(0, 0, 0, 0.2)", 
        display: isLoading ? "flex" : "none",
        justifyContent: "center",
        alignItems: "center",
        zIndex: 9999, 
      }}
    >
      <CircularProgress disableShrink={true} />
    </div>
  );
};

export default Loading;
