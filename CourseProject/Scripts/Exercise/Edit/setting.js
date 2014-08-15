(function() {
  $(function() {
    var $answers, $equations, $images, $jqXHRData, $tags, createButton, createChildDiv, createFormulaElement, createImageElement, createInput, createParentDiv, getFormulas, getImages, initFileUpload;
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
      var childButton, childDivForButton, childDivForImage, parent;
      parent = createParentDiv();
      childDivForImage = createChildDiv();
      childDivForImage.className = "col-md-4";
      childDivForButton = createChildDiv();
      childDivForImage.innerHTML = formula;
      childDivForImage.setAttribute('name', 'AddEquation');
      childButton = createButton("delete", "Delete");
      childDivForButton.appendChild(childButton);
      parent.appendChild(childDivForImage);
      parent.appendChild(childDivForButton);
      return parent;
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
    getFormulas = function() {
      var elements;
      elements = $("[name='AddEquation']");
      $.each(elements, function(e, value) {
        var img;
        img = value.children[0];
        return $equations.push(img.src);
      });
      return $equations;
    };
    getImages = function() {
      var im;
      im = $("#listOfPictures > .row > .thumbnail > img");
      $.each(im, function(e, val) {
        return $images.push(val.src);
      });
      return $images;
    };
    return $(document).ready(function() {
      $(".md-header").css('height', '35px');
      $(".jumbotron")[0].style.setProperty("height", "250px");
      $(document).on('click', '#addFormula', function(e) {
        var child, img, list;
        e.stopPropagation();
        e.preventDefault();
        img = $('#equationToImg')[0];
        list = $("#listOfFormulas")[0];
        if (img.value) {
          child = createFormulaElement(img.value);
          $('#equationToImg')[0].value = "";
          $('#equation')[0].src = "";
          $('#equationInput')[0].value = "";
          list.appendChild(child);
        }
        return null;
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
        var answers, equation, images, tags;
        answers = $("#inputAnswers").textext()[0].hiddenInput().val();
        images = getImages();
        equation = getFormulas();
        equation = JSON.stringify(equation);
        tags = $("#inputTags").textext()[0].hiddenInput().val();
        $('input#Answers')[0].value = answers;
        $('input#Pictures')[0].value = images;
        $('input#Tags')[0].value = tags;
        $('input#Equations').val(equation);
        $("input#Name")[0].value = $("#Exercise_Name").val();
        return $("input#Text")[0].value = $("[name='Exercise.Text']").val();
      });
      return null;
    });
  });

}).call(this);

//# sourceMappingURL=setting.js.map
