import React from 'react';
import { useState, useEffect } from 'react';
import Navbar from "../../../layouts/Navbar";
import Sidebar from "../../../layouts/Sidebar";
import Footer from "../../../layouts/Footer";
import style from "../../../../pages/DashboardPage/style.module.scss";
import ReClassDialog from "./ReClassDialog";
import ReClassRecommend from "./ReClassRecommend";
import { useNavigate, useParams } from 'react-router-dom';
import { toast } from 'react-toastify';


function ReClass() {
    const navigate = useNavigate();
    const { reservedClassId, message } = useParams();
    useEffect(() => {
        if (message == "ReClassSuccessful") {
            toast.success("Re class successful")
            navigate(`/reclass/${reservedClassId}/null`)
        }
    }, [message])



    return (
            <div style={{ marginLeft: '15%' }}>
                <ReClassDialog reservedClassId={reservedClassId} />
                <ReClassRecommend />
            </div>
    );
}

export default ReClass;