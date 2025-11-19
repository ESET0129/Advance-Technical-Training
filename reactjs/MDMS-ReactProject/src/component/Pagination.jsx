import React from 'react';
import '../styles/Table.css'; 


const getPaginationRange = (currentPage, totalPages) => {
  const delta = 2; 
  const range = [];
  for (let i = 1; i <= totalPages; i++) {
    if (i === 1 || i === totalPages || (i >= currentPage - delta && i <= currentPage + delta)) {
      range.push(i);
    }
  }

  const rangeWithDots = [];
  let l;
  for (const i of range) {
    if (l) {
      if (i - l === 2) {
        rangeWithDots.push(l + 1);
      } else if (i - l !== 1) {
        rangeWithDots.push('...');
      }
    }
    rangeWithDots.push(i);
    l = i;
  }
  return rangeWithDots;
};

export default function Pagination({ currentPage, totalPages, onPageChange }) {
  const pages = getPaginationRange(currentPage, totalPages);

  return (
    <div className="pagination">
      <button
        className="pagination-btn"
        onClick={() => onPageChange(currentPage - 1)}
        disabled={currentPage === 1}
      >
        ← Previous
      </button>
      
      {pages.map((page, index) => (
        <span
          key={index}
          className={`pagination-item ${page === '...' ? 'pagination-dots' : ''} ${page === currentPage ? 'active' : ''}`}
          onClick={() => typeof page === 'number' && onPageChange(page)}
        >
          {page}
        </span>
      ))}
      
      <button
        className="pagination-btn"
        onClick={() => onPageChange(currentPage + 1)}
        disabled={currentPage === totalPages}
      >
        Next →
      </button>
    </div>
  );
}