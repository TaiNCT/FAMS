import React from "react";
import { Flex, Tag, TagCloseButton, TagLabel } from "@chakra-ui/react";

const TagList = ({ tags, setTags }) => (
  <Flex px={8} gap={2} flexWrap={"wrap"}>
    {tags.map((tag, index) => {
      const key = `tag-${tag}-${index}`;
      return (
        <Tag borderRadius="5px" variant="solid" bgColor="#2D3748" key={key} marginBottom={'1em'}>
          <TagLabel fontSize="12px">
            {tag}
          </TagLabel>
          <TagCloseButton
            onClick={() => {
              const updatedTags = tags.filter((t) => t !== tag);
              setTags(updatedTags);
              localStorage.setItem("tags", JSON.stringify(updatedTags));
            }}
          />
        </Tag>
      );
    })}
  </Flex>
);

export default TagList;
