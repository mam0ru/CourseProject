(function() {
  var $editor;

  $editor = null;

  window.onload = function() {
    $editor = com.wiris.jsEditor.JsEditor.newInstance({
      'language': 'en'
    });
    return $editor.insertInto(document.getElementById('editorContainer'));
  };

  $(function() {
    window.onload();
    return $('#addFormula').on('click', function(e) {
      var displayMathML, item;
      e.stopPropagation();
      e.preventDefault();
      alert("qwerty");
      item = document.createElement('p');
      item.id = "formula";
      item.innerHTML = $editor.getMathML();
      displayMathML = new mdgw.mathml.DisplayMathML();
      displayMathML.replaceAll(document);
      return $('#listOfFormulas').append(item);
    });
  });

}).call(this);

//# sourceMappingURL=addFormulas.js.map
