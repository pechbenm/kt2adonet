const editor = document.getElementById('editor');

document.querySelectorAll('[data-tag]').forEach(button => {
    button.addEventListener('click', () => {
        const tag = button.dataset.tag;
        const start = editor.selectionStart;
        const end = editor.selectionEnd;
        const selected = editor.value.substring(start, end);
        const open = `<${tag}>`;
        const close = `</${tag}>`;

        editor.setRangeText(open + selected + close, start, end, 'end');

        // если ничего не выделено, то крурсор ставится между тегами 
        if (start === end) {
            const pos = start + open.length;
            editor.setSelectionRange(pos, pos);
        }
        editor.focus();
    });
});