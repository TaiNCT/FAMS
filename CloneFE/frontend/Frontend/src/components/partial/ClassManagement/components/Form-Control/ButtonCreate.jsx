import { Button } from "@mui/material";
function ButtonCreate({ name, onClick }) {
  return (
    <div>
      <div style={{ width: "100px" }}>
        <Button
          style={{
            backgroundColor: "#2d3748",
            color: "white",
            textTransform: "none",
            borderRadius: "10px",
            width: "100%",
          }}
          variant="text"
          onClick={onClick}
        >
          {name}
        </Button>
      </div>
    </div>
  );
}

export default ButtonCreate;
