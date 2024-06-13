import Input from "../../../components/Form-Control/Input";
function ClassInput({ onchange, error, msgClassName })
{

  const handleChange = (e) =>
  {
    onchange(e);
    
  };

  return (
    <div>
      <div style={{ display: "flex", alignItems: "center" }}>
        <div
          style={{
            marginRight: "10px",
            marginLeft: "15px",
            fontWeight: "bold",
          }}
        >
          Class name
        </div>
        <Input name={" Name the class"} onChange={handleChange} error={error} msgClassName={msgClassName} />
      </div>
    </div>
  );
}

export default ClassInput;
