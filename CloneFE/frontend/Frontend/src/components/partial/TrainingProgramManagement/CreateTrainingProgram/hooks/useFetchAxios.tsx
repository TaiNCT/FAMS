import { useEffect, useState } from "react";
import axiosAuth from "../../api/axiosAuth";

const useFetchAxios = () => {
  const [data, setData] = useState([]);
  const [fetchError, setFetchError] = useState<string | null>(null);
  const [isLoading, setIsLoading] = useState<boolean>(false);

  useEffect(() => {
    let isMounted = true;
    //cancel token
    // const source = axiosAuth.CancelToken.source();

    const fetchData = async () => {
      setIsLoading(true);

      try {
        const response = await axiosAuth.get(`trainingprograms/syllabi`, {
          // cancelToken: source.token,
        });

        if (isMounted) {
          setData(response.data);
          setFetchError("");
        }
      } catch (err: unknown) {
        if (err instanceof Error) {
          if (isMounted) {
            setFetchError(err.message);
          }
        }
      } finally {
        isMounted && setTimeout(() => setIsLoading(false), 1000);
      }
    };

    fetchData();

    const cleanUp = () => {
      isMounted = false;
      // source.cancel();
    };

    return cleanUp;
  }, []);

  return { data, fetchError, isLoading };
};

export default useFetchAxios;
