import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import GlobalLoading from "../../global/GlobalLoading";
import { createTheme, Pagination, TablePagination, ThemeProvider } from "@mui/material";

interface Response {
  statusCode: number;
  message: string;
  isSuccess: boolean;
  data: LearningMaterial[];
  errors: any;
}

interface LearningMaterial {
  id: number;
  trainingMaterialId: string;
  createdBy: string | null;
  createdDate: string | null;
  isDeleted: boolean;
  modifiedBy: string | null;
  modifiedDate: string | null;
  fileName: string;
  isFile: boolean;
  name: string;
  url: string | null;
  unitChapterId: string;
}

const LearningMaterial = () => {
  // @ts-ignore
  const apiBaseUrl = `${import.meta.env.VITE_API_HOST}:${import.meta.env.VITE_API_PORT
    }`;
  const [learningMaterial, setLearningMaterial] = useState<Response>();
  const [showLearningMaterial, setShowLearningMaterial] = useState<LearningMaterial[]>([]);
  const [isLoading, setIsLoading] = useState(false);
  const [page, setPage] = useState(1);
  const [rowsPerPage, setRowsPerPage] = useState(5);

  const handleChangePage = (event: React.ChangeEvent<unknown>, value: number) => {
    setPage(value);
  };

  const handleRowPerPage = (event: React.ChangeEvent<HTMLInputElement>) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(1);
  }

  const handleDownload = async (id: string, fileName: string) => {
    try {
      const response = await fetch(
        `${apiBaseUrl}/api/trainingprograms/materials/${id}`,
        {
          method: "GET",
          headers: {
            "Content-Type": "application/octet-stream",
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        }
      );
      if (response.ok) {
        const blob = await response.blob();
        toast.success(`Downloading file`, {
          position: "top-right",
          autoClose: 5000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
          theme: "light",
        });
        const url = window.URL.createObjectURL(new Blob([blob]));
        const link = document.createElement("a");
        link.href = url;
        link.download = fileName || "downloaded-file";
        document.body.appendChild(link);

        link.click();

        document.body.removeChild(link);
        window.URL.revokeObjectURL(url);
      } else {
        const data = (await response.json()) as Response;
        toast.error(`${data.message}. Code: ${response.status}`, {
          position: "top-right",
          autoClose: 5000,
          hideProgressBar: false,
          closeOnClick: true,
          pauseOnHover: true,
          draggable: true,
          progress: undefined,
          theme: "light",
        });
      }
    } catch (error) {
      console.error(error);
    }
  };

  const theme = createTheme({
    palette: {
      primary: {
        main: "#285D9A",
      },
      secondary: {
        main: "#DFDEDE",
      },
    },
    typography: {
      fontFamily: ["Inter"].join(","),
    },
    breakpoints: {
      values: {
        xs: 0,
        sm: 600,
        md: 900,
        lg: 1200,
        xl: 1536,
      },
    },
  });

  const getLearningMaterial = async () => {
    setIsLoading(true);
    try {
      const response = await fetch(
        `${apiBaseUrl}/api/trainingprograms/materials`,
        {
          method: "GET",
          headers: {
            Authorization: `Bearer ${localStorage.getItem("token")}`,
          },
        }
      );
      const data = (await response.json()) as Response;
      setLearningMaterial(data);
      setIsLoading(false);
    } catch (error) {
      setIsLoading(false);
      console.error(error);
    }
  };

  useEffect(() => {
    if (learningMaterial) return;
    getLearningMaterial();
  }, []);

  useEffect(() => {
    if (learningMaterial) {
      const start = (page - 1) * rowsPerPage;
      const end = start + rowsPerPage;
      setShowLearningMaterial(learningMaterial.data.slice(start, end));
    }
  }, [page, learningMaterial, rowsPerPage]);

  return (
    <div className="flex flex-col min-h-screen overflow-y-hidden">
      <h3
        style={{
          color: "#fff",
          padding: "30px",
          width: "100%",
          margin: "2px 0px 30px",
          alignContent: "center",
          height: "80px",
          background: "#2d3748",
          fontSize: "32px",
          letterSpacing: "6px",
        }}
      >
        Learning Material
      </h3>
      {isLoading ? <GlobalLoading isLoading={isLoading} /> : null}
      {!learningMaterial ? (
        <div className="text-lg text-center mt-5">
          No learning material available
        </div>
      ) : (
        showLearningMaterial?.map((material) => (
          <div
            className="border bg-card text-card-foreground shadow-sm flex space-y-0 w-full rounded-2xl text-nowrap px-6 py-3 mb-2 mt-5"
          >
            <div className="flex-1">
              <div className="font-semibold font-inter text-xl text-primary cursor-pointer">
                {material.name}
              </div>
              <div className="text-[12px] space-y-0 pb-0 font-semibold text-[#8B8B8B]">
                {material.modifiedBy ? material.modifiedBy : material.createdBy}
              </div>
              <div className="text-[14px]">
                Modified on{" "}
                {material.modifiedDate
                  ? material.modifiedDate
                  : material.createdDate}{" "}
                by{" "}
                {material.modifiedBy ? material.modifiedBy : material.createdBy}
              </div>
            </div>
            <div className="flex flex-col items-center justify-between text-sm">
              <button
                style={{ color: "white", backgroundColor: "#0f172a" }}
                onClick={() =>
                  handleDownload(material.trainingMaterialId, material.fileName)
                }
                className="inline-flex items-center justify-center whitespace-nowrap rounded-md text-sm font-medium ring-offset-background transition-colors focus-visible:outline-none focus-visible:ring-2 focus-visible:ring-ring focus-visible:ring-offset-2 disabled:pointer-events-none disabled:opacity-50 bg-primary text-primary-foreground hover:bg-primary/90 h-10 px-4 py-2"
              >
                Download
              </button>
            </div>
          </div>
        ))
      )}
      <ThemeProvider theme={theme}>
        <div style={{ justifyContent: 'center', display: 'flex', alignItems: 'center', margin: '1em 0 1em 0' }}>
          <Pagination count={Math.ceil(learningMaterial?.data.length / rowsPerPage)}
            page={page}
            onChange={handleChangePage}
            color="primary"
          />
          <TablePagination
            component="div"
            count={Math.ceil(learningMaterial?.data.length / rowsPerPage)}
            page={page}
            rowsPerPage={rowsPerPage}
            onPageChange={handleChangePage}
            onRowsPerPageChange={handleRowPerPage}
            rowsPerPageOptions={[5, 10, 25, 50]}
          />
        </div>
      </ThemeProvider>
    </div>
  );
};

export default LearningMaterial;
