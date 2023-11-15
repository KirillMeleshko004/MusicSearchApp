import React, { useRef, useState } from "react";
import BorderedTextInput from "../textInput.jsx";


function Upload()
{
    
    const [selectedImage, setSelectedImage] = useState(null);

    const image = useRef(null);
    const imageField = useRef(null);

    const textField = useRef(null);

    function imageChanged(event)
    {
        setSelectedImage(event.target.files[0]);
        let url = URL.createObjectURL(event.target.files[0]);
        image.current.src = url;
    }

    function test()
    {
        console.log(textField.current);
    }

    const props =
    {
        placeholder: "Test input",
        maxLength: 24,
    }
    
    return (
        <section className="panel large-padded large-gaped vertical fill-space full-height">
            <div className="largest horizontal center-justified">
                Creating New Album
            </div>
            <form className="medium-gaped vertical fill-space">

                <BorderedTextInput ref={textField} {...props}>

                </BorderedTextInput>
            </form>
        </section>
    );
}

export default Upload;


