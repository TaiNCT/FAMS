import React from 'react';
import './Popup.scss'; // Import the CSS styles

function FailRemove() {
  return (
    <div className="popup">
      <div className="red-line"></div> {/* Red vertical line */}
      
        <svg width="20" height="20" viewBox="0 0 20 20" fill="none" xmlns="http://www.w3.org/2000/svg" className='icon'>
        <path d="M10 20C15.5234 20 20 15.5234 20 10C20 4.47656 15.5234 0 10 0C4.47656 0 0 4.47656 0 10C0 15.5234 4.47656 20 10 20ZM10.9375 14.0625C10.9375 14.5781 10.5156 15 10 15C9.48438 15 9.0625 14.5801 9.0625 14.0625L9.0625 9.0625C9.0625 8.54492 9.48242 8.125 10 8.125C10.5176 8.125 10.9375 8.54297 10.9375 9.0625L10.9375 14.0625ZM10 4.375C10.6781 4.375 11.2281 4.925 11.2281 5.60312C11.2281 6.28125 10.6785 6.83125 10 6.83125C9.32148 6.83125 8.77188 6.28125 8.77188 5.60312C8.77344 4.92578 9.32031 4.375 10 4.375Z" fill="#E74A3B"/>
        </svg>



      <div className="content">
        <h3 className="message">Something wrong!</h3>
        <p className="light-text">Please try again.</p>
      </div>
    </div>
  );
}

export default FailRemove;