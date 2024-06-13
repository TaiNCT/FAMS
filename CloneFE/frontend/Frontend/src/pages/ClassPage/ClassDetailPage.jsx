import Navbar from "../../components/layouts/Navbar";
import Sidebar from "../../components/layouts/Sidebar";
import ClassDetail from "../../components/partial/ClassManagement/features/ClassDetail";
import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { GetClassInfo } from "../../components/partial/ClassManagement/features/ClassList/api/ListApi";
import { set } from "../../lib/redux/classSlice";
import { useDispatch } from "react-redux";
import Footer from "../../components/layouts/Footer";
const ClassDetailPage = () => {
  const { classId } = useParams();
  const dispatch = useDispatch();
  useEffect(() => {
    async function fetchData() {
      try {
        if (classId) {
          const response = await GetClassInfo(classId);
          dispatch(set(response));
        }
      } catch (error) {
      }
    }
    if (classId) {
      fetchData();
    }
  }, [classId]);
  return (
    <div className="min-h-screen flex flex-col">
      <Navbar />
      <div className="flex-1 flex">
        <Sidebar />
        <ClassDetail />
      </div>
      <Footer />
    </div>
  );
};

export default ClassDetailPage;
