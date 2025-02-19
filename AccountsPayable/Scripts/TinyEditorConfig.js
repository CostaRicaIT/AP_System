tinymce.init({
    selector: 'textarea.TinyEditorReadOnly', //select field using textarea.class
    promotion: false, //Hide upgrade promotion of TinyMCE
    branding: false, //Hide TinyMCE logo
    readonly: true,
    statusbar: false,
    menubar: false,
    toolbar: false,
    license_key: 'gpl'
    
});
tinymce.init({
    selector: '#form-TaxID, #form-Folder, #form-RemitToAccount, #form-DistroCombination, #form-DistroSet', // Targets multiple form IDs
    promotion: false, // Hide upgrade promotion of TinyMCE
    branding: false, // Hide TinyMCE logo
    statusbar: false,
    menubar: false,
    toolbar: "undo redo |forecolor |backcolor",
    license_key: 'gpl',
    plugins: 'autoresize',
    autoresize_bottom_margin: 0,
    autoresize_max_height: 10, // Adjust to fit a single line
    content_style: "white-space: nowrap; overflow: hidden; margin-left: 1000px; margin-right: 0; position: relative;", // Add margin-left and position to move entire editor
    width: '70%'  // Adjust width to 100% of its container or specify a pixel value (e.g., 500px)
});
tinymce.init({
    selector: 'textarea.TinyEditor:not(#form-TaxID, #form-Folder, #form-RemitToAccount, #form-DistroCombination, #form-DistroSet)', // Exclude specific forms
    promotion: false, //Hide upgrade promotion of TinyMCE
    branding: false, //Hide TinyMCE logo
    statusbar: false,
    menubar: false,
    toolbar: "undo redo |forecolor |backcolor ",
    license_key: 'gpl'
});