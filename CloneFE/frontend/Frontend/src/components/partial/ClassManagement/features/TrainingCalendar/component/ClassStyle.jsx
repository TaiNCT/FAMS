import React from "react";

export default function ClassStyle({ background, children }) {
  return <div style={{ background: background }}>{children}</div>;
}
