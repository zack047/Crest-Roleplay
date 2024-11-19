import axios from "axios";

const clientId = "f9025d6b7d57367";

const uploadToImgur = async (base64Image) => {
  try {
    const response = await axios.post(
      "https://api.imgur.com/3/image",
      {
        image: base64Image,
      },
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Client-ID ${clientId}`,
        },
      }
    );

    if (response.status === 200) {
      const link = response.data.data.link;
      window.listernEvent("cameraLink", link);
    }
  } catch (error) {
    console.error("Failed to upload image to Imgur:", error);
  }
};

export default uploadToImgur;
