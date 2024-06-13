// Kiểm tra độ dài của chuỗi
export const isLengthAtLeast = (str: string, length: number) => {
    return str.length >= length;
  };
  
  // Kiểm tra xem chuỗi có phải là số không
  export const isNumeric = (str: string) => {
    return /^\d+$/.test(str);
  };