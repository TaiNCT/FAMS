/* eslint-disable react/prop-types */
import { useState } from "react";
import "./SwitchButton.css"
const SwitchButton = ({ onToggle, checked = false }) => {
    const [isChecked, setIsChecked] = useState(checked);
    return (
        <div className="toggle-button-cover" onClick={onToggle} >
            <div className="button b2" id="btnStatus">
                <input type="checkbox" className="checkbox" checked={!isChecked} onChange={() => setIsChecked(!isChecked)} />
                <div className="knobs">
                    <span></span>
                </div>
                <div className="layer"></div>
            </div>
        </div>
    )
}

export default SwitchButton