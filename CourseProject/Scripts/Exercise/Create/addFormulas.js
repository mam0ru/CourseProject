(function() {
  var editor;

  editor = null;

  window.onload = function() {
    editor = com.wiris.jsEditor.JsEditor.newInstance({
      'language': 'en'
    });
    return editor.insertInto(document.getElementById('editorContainer'));
  };

  $(function() {
    window.onload();
    return $('#addFormula').on('click', function(e) {
      var item;
      e.stopPropagation();
      e.preventDefault();
      item = document.createElement('p');
      item.id = "formula";
      item.innerHTML = editor.getMathML();
      return $('#formulas').append(item);
    });
  });

}).call(this);

//# sourceMappingURL=addFormulas.js.map
