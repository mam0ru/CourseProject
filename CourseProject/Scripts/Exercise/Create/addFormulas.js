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
    return $('#addFormula').on('click', function() {
      var jsItem;
      jsItem = document.createElement('p');
      jsItem.innerHTML = editor.getMathML();
      return $('#formulas').append(jsItem);
    });
  });

}).call(this);

//# sourceMappingURL=addFormulas.js.map
