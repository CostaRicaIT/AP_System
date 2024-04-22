tinymce.init({
    selector: 'textarea.TinyEditorReadOnly', //select field using textarea.class
    promotion: false, //Hide upgrade promotion of TinyMCE
    branding: false, //Hide TinyMCE logo
    readonly: true,
    statusbar: false,
    menubar: false,
    toolbar: false,
    
});
tinymce.init({
    selector: 'textarea.TinyEditor', //select field using textarea.class
    promotion: false, //Hide upgrade promotion of TinyMCE
    branding: false, //Hide TinyMCE logo
    statusbar: false,
    menubar: false,
    toolbar: "undo redo |forecolor |backcolor "
});