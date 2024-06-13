import React from 'react';
import './Popup.scss'; // Import the CSS styles

function SuccessRemove() {
  return (
    <div className="popup">
      <div className="green-line"></div> {/* Red vertical line */}
      
        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-check-circle-fill" viewBox="0 0 16 16" className = "green-svg">
        <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0m-3.97-3.03a.75.75 0 0 0-1.08.022L7.477 9.417 5.384 7.323a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-.01-1.05z"/>
        </svg>



      <div className="content">
        <h3 className= "message" >Remove Successful!!</h3>
        <p className="light-text">Your changes has been saved.</p>
      </div>
    </div>
  );
}

export default SuccessRemove;