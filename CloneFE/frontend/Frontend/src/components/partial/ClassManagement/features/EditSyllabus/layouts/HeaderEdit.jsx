import React from "react";
function HeadClassPage({ children }) {
  React.useEffect(() => {
    const warnUser = () => {
      if (window.confirm("Bạn có chắc muốn làm mới trang này?")) {
        return;
      } else {
        event.preventDefault();
      }
    };

    window.addEventListener("beforeunload", warnUser);

    return () => {
      window.removeEventListener("beforeunload", warnUser);
    };
  }, []);
  return <div>{children}</div>;
}
export default HeadClassPage;
