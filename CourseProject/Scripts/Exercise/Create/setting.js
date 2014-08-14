(function() {
  $(function() {
    var $answers, $equations, $images, $jqXHRData, $tags, createAnswerInput, createButton, createChildDiv, createFormulaElement, createImageElement, createInput, createParentDiv, createTagInput, editor, getAnswers, getImages, getTags, initFileUpload;
    createParentDiv = function() {
      var parent;
      parent = document.createElement('div');
      parent.className = 'row';
      return parent;
    };
    createChildDiv = function() {
      var childDiv;
      childDiv = document.createElement('div');
      childDiv.className = 'col-md-4';
      return childDiv;
    };
    createInput = function(name) {
      var input;
      input = document.createElement('input');
      input.type = 'text';
      input.name = name;
      return input;
    };
    createButton = function(name, text) {
      var button;
      button = document.createElement('button');
      button.name = name;
      button.innerText = text;
      return button;
    };
    createTagInput = function() {
      var childButton, childDivForButton, childDivForInput, childInput, parent;
      parent = createParentDiv();
      childInput = createInput("Tag");
      childInput.setAttribute('data-autocomplete-source', "/Exercise/TagAutocompliteSearch");
      childDivForInput = createChildDiv();
      childDivForButton = createChildDiv();
      childDivForInput.appendChild(childInput);
      childButton = createButton("delete", "Delete");
      childDivForButton.appendChild(childButton);
      parent.appendChild(childDivForInput);
      parent.appendChild(childDivForButton);
      $('#listOfTags').append(parent);
      return null;
    };
    createAnswerInput = function() {
      var childButton, childDivForButton, childDivForInput, childInput, parent;
      parent = createParentDiv();
      childInput = createInput("answer");
      childDivForInput = createChildDiv();
      childDivForButton = createChildDiv();
      childDivForInput.appendChild(childInput);
      childButton = createButton("delete", "Delete");
      childDivForButton.appendChild(childButton);
      parent.appendChild(childDivForInput);
      parent.appendChild(childDivForButton);
      $('#listOfAnswers').append(parent);
      return null;
    };
    $jqXHRData = null;
    createImageElement = function(src) {
      var childButton, childDivForButton, childDivForImage, childImage, parent;
      parent = createParentDiv();
      childImage = document.createElement('img');
      childImage.src = src;
      childDivForImage = createChildDiv();
      childDivForImage.className = "col-md-4 thumbnail";
      childDivForButton = createChildDiv();
      childDivForImage.appendChild(childImage);
      childButton = createButton("delete", "Delete");
      childDivForButton.appendChild(childButton);
      parent.appendChild(childDivForImage);
      parent.appendChild(childDivForButton);
      return parent;
    };
    createFormulaElement = function(formula) {
      var childButton, childDivForButton, childDivForImage, childFormula, parent;
      parent = createParentDiv();
      childFormula = document.createElement('p');
      childFormula.innerHTML = formula;
      childDivForImage = createChildDiv();
      childDivForImage.className = "col-md-4";
      childDivForButton = createChildDiv();
      childDivForImage.appendChild(childFormula);
      childButton = createButton("delete", "Delete");
      childDivForButton.appendChild(childButton);
      parent.appendChild(childDivForImage);
      parent.appendChild(childDivForButton);
      return parent;
    };
    editor = null;
    window.onload = function() {
      editor = com.wiris.jsEditor.JsEditor.newInstance({
        'language': 'en'
      });
      return editor.insertInto(document.getElementById('editorContainer'));
    };
    initFileUpload = function() {
      return $('#imageupload').fileupload({
        url: '/Exercise/UploadImage',
        dataType: 'json',
        add: function(e, data) {
          return $jqXHRData = data;
        },
        done: function(event, data) {
          var jsItem;
          alert("file uploaded");
          jsItem = createImageElement(data.result.path);
          return $('#listOfPictures').append(jsItem);
        },
        fail: function(event, data) {
          alert("ERROR");
          if (data.files[0].error) {
            return alert(data.files[0].error);
          }
        }
      });
    };
    $equations = [];
    $images = [];
    $tags = [];
    $answers = [];
    getImages = function() {
      var im;
      im = $("#listOfPictures > .row > .thumbnail > img");
      $.each(im, function(e, val) {
        return $images.push(val.src);
      });
      return $images;
    };
    getTags = function() {
      var t;
      t = $("#listOfTags > .row > div > [name='Tag']");
      if (t.length === 0) {
        return null;
      }
      $.each(t, function(e, val) {
        return $tags.push(val.value);
      });
      return $tags;
    };
    getAnswers = function() {
      var ans;
      ans = $("#listOfAnswers > .row > div > [name='answer']");
      $.each(ans, function(e, val) {
        return $answers.push(val.value);
      });
      return $answers;
    };
    return $(document).ready(function() {
      window.onload();
      $('#addFormula').on('click', function(e) {
        var item;
        e.stopPropagation();
        e.preventDefault();
        item = createFormulaElement(editor.getMathML());
        return $('#listOfFormulas').append(item);
      });
      $(document).on('click', '#addAnswer', function(e) {
        e.stopPropagation();
        e.preventDefault();
        createAnswerInput();
        return null;
      });
      $(document).on('click', "[name='delete']", function(e) {
        var parent1, parent2;
        e.stopPropagation();
        e.preventDefault();
        $(this).unbind();
        parent1 = $(this).parent();
        parent2 = parent1.parent();
        parent2.remove();
        return null;
      });
      $(document).on('click', "#addTag", function(e) {
        e.stopPropagation();
        e.preventDefault();
        createTagInput();
        $("[data-autocomplete-source]").each(function() {
          var target;
          target = $(this);
          return target.autocomplete({
            source: target.attr("data-autocomplete-source")
          });
        });
        return null;
      });
      $("[data-autocomplete-source]").each(function() {
        var target;
        target = $(this);
        target.autocomplete({
          source: target.attr("data-autocomplete-source")
        });
        return null;
      });
      initFileUpload();
      $(document).on('click', '#Upload', function(e) {
        e.stopPropagation();
        e.preventDefault();
        if ($jqXHRData) {
          alert("upload");
          $jqXHRData.submit();
        }
        return false;
      });
      $("#Submit").on('click', function(e) {
        var answers, images, tags;
        alert("submit");
        answers = getAnswers();
        images = getImages();
        tags = getTags();
        $('input#Category').val($("select#Category").val());
        $('input#Answers').val(answers);
        $('input#Pictures').val(images);
        return $('input#Tags').val(tags);
      });
      return null;
    });
  });

}).call(this);

//# sourceMappingURL=setting.js.map
