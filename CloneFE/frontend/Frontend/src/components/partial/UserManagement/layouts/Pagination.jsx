import React from "react";
import { GrFormNext, GrFormPrevious } from "react-icons/gr"; 
import { MdFirstPage, MdLastPage } from "react-icons/md";
import "./Pagination.css";

const Pagination = ({
  totalPosts,
  postsPerPage,
  setCurrentPage,
  currentPage,
}) => {
  const totalPages = Math.ceil(totalPosts / postsPerPage);
  let pages = [];

  let startPage = Math.max(1, currentPage - 1);
  let endPage = Math.min(totalPages, currentPage + 1);

  for (let i = startPage; i <= endPage; i++) {
    pages.push(i);
  }

  return (
    <div className="pagination">
      {currentPage > 1 && (
        <>
          <button className="previous" style={{backgroundColor:"transparent"}} onClick={() => setCurrentPage(1)}>
          <MdFirstPage size="24px"/> {/* Assuming GrPrevious or use CSS to rotate GrNext */}
          </button>
          <button className="next" style={{backgroundColor:"transparent"}} onClick={() => setCurrentPage(currentPage - 1)}>
            <GrFormPrevious size="24px"/> {/* Use rotated GrNext if GrPrevious is not available */}
          </button>
        </>
      )}

      {startPage > 1 && (
        <>
          <button onClick={() => setCurrentPage(1)}>1</button>
          {startPage > 2 && <span>...</span>}
        </>
      )}

      {pages.map((page, index) => (
        <button
          key={index}
          onClick={() => setCurrentPage(page)}
          className={page === currentPage ? "active" : ""}
        >
          {page}
        </button>
      ))}

      {endPage < totalPages && (
        <>
          {endPage < totalPages - 1 && <span>...</span>}
          <button onClick={() => setCurrentPage(totalPages)}>{totalPages}</button>
        </>
      )}

      {currentPage < totalPages && (
        <>
          <button className="next" style={{backgroundColor:"transparent"}} onClick={() => setCurrentPage(currentPage + 1)}>
            <GrFormNext size="24px"/>
          </button>
          <button className="last-page" style={{backgroundColor:"transparent"}} onClick={() => setCurrentPage(totalPages)}>
            <MdLastPage size="24px"/>{/* Consider rotating this icon for a "Last" indication if needed */}
          </button>
        </>
      )}
    </div>
  );
};

export default Pagination;
