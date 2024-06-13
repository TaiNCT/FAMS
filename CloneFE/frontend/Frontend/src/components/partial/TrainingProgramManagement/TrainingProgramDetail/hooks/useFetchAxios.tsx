import { useCallback, useEffect, useState } from "react";
import axiosAuth from "../../api/axiosAuth";
import { TrainingProgram } from "../models/trainingprogram.model";

const useAxiosFetch = (code: string) => {
  const [trainingProgramData, setTrainingProgramData] =
    useState<TrainingProgram>(null!);
  const [fetchError, setFetchError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(false);

  const refreshData = useCallback(async () => {

    const isMounted = true;
    const fetchData = async () => {
      setIsLoading(true);
      try {
        const response = await axiosAuth.get(`trainingprograms/${code}`, {
          // cancelToken: source.token,
        });
        if (isMounted) {
          setTrainingProgramData(response.data.data);
          setFetchError("");
        }
      } catch (err) {
        // if (!axios.isCancel(err) && isMounted) {
        //   setFetchError(err.message);
        // }
        if (isMounted) setFetchError(err.message);
      } finally {
        if (isMounted) {
          setTimeout(() => setIsLoading(false), 1000);
        }
      }
    };

    fetchData();
  }, []);

  useEffect(() => {
    let isMounted = true;
    // const source = axios.CancelToken.source();

    const fetchData = async () => {
      setIsLoading(true);
      try {
        const response = await axiosAuth.get(`trainingprograms/${code}`, {
          // cancelToken: source.token,
        });
        if (isMounted) {
          setTrainingProgramData(response.data.data);
          setFetchError("");
        }
      } catch (err) {
        // if (!axios.isCancel(err) && isMounted) {
        //   setFetchError(err.message);
        // }
        if (isMounted) setFetchError(err.message);
      } finally {
        if (isMounted) {
          setTimeout(() => setIsLoading(false), 1000);
        }
      }
    };

    fetchData();

    return () => {
      isMounted = false;
      // Cancel request only if component is unmounting explicitly
      if (typeof window !== "undefined" && window.location) {
        // Check if the component is being unmounted during a page reload
        const isPageReload = !!(
          window.performance &&
          window.performance.navigation &&
          window.performance.navigation.type ===
            window.performance.navigation.TYPE_RELOAD
        );
        if (!isPageReload) {
          // source.cancel("Component unmounted");
        }
      }
    };
  }, [code, refreshData]);

  return {
    trainingProgramData,
    setTrainingProgramData,
    fetchError,
    isLoading,
    setIsLoading,
    refreshData,
  };
};

export default useAxiosFetch;
