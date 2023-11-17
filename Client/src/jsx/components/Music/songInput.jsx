import React, { forwardRef, useRef, useState } from 'react'

const SongInput = forwardRef( function(props, ref) {

    const audio = useRef(null);
    const input = useRef(null);
    const [file, setFile] = useState(null);
    
    function onFileChanged()
    {
        if(!input.current.files[0]){ 
            return;
        }
        let url =  URL.createObjectURL(input.current.files[0]);
        setFile(input.current.files[0].name);
        audio.current.src = url;

        if(props.hasOwnProperty('onChange')) props?.onChange(input);
    }
    
    return (

        <div className='vertical center-aligned medium-gaped medium-padded bordered-block fill-space'>
            <div className='normal' style={{height:"30%"}}>
                {file}
            </div>
            <audio className='full-width'
                controls src="https://dl2.mp3party.net/online/10101028.mp3"
                ref={audio}
                controlsList="nodownload"></audio>
            <label htmlFor='audioFileInput'
                className="bordered-block horizontal center-aligned center-justified 
                red-border-on-hover full-width large-spaced unselectable medium-padded 
                normal">
                Choose audio file
            </label>
            <input 
                id='audioFileInput'
                type='file'
                accept=".mp3"
                className='non-displayed'
                ref={input}
                onChange={onFileChanged}/>
                
        </div>
            
    )

}
)

export default SongInput;